using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MLMProject.Application.Common;
using MLMProject.Application.DTOs;
using MLMProject.Application.Interfaces;
using MLMProject.Application.IService;
using MLMProject.Domain.Entities;
using RoadMech.Mechanic.API.Models.Utility;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MLMProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IEmailService emailservice) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IEmailService _emailService = emailservice;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        #region UserAuth

        /// <summary>
        /// This method for Login User side.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                var user = await _userService.Login(loginRequest);
                if (user.Data != null)
                {
                    if (user.Data.IsLocked)
                    {
                        return Unauthorized(new { Message = ResponseConstants.Multiplelogin });
                    }
                    if (user.Success && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Data.Password))
                    {
                        var token = _tokenService.GenerateToken(user.Data);
                        HttpContext.Session.SetString("token", token);
                        var otp = CommonMethods.GenerateOtpForLogin(_httpContextAccessor.HttpContext);
                        var log = new LoginLog
                        {
                            UserId = user.Data.UserId,
                            IPAddress = GetClientIpAddress(),
                            UserAgent = GetClientUserAgent(),
                            LoginTime = DateTime.UtcNow,
                            OTP = (int)Convert.ToUInt32(otp["otp"]),
                            OTPID = otp["otpId"],
                            Token = token,
                        };
                        await _userService.AddLoginLogAsync(log);
                        if (token != null)
                        {
                            user.Data.FailedLoginAttempts = 0;
                            user.Data.IsLocked = false;
                            Guid userId = user.Data.UserGuid;
                            await _userService.UpdateUserONLogin(user.Data);
                            // Construct the email body with string interpolation
                            string otps = otp["otp"];
                            string emailBody = await _emailService.GenerateEmailBody(user.Data.Name, otp["otp"], otp["otpId"]);
                            //string emailBody = $"Dear {user.Data.Name}\n\n,\n\nYour OTP for login is: {otp["otp"]} for OTP-ID: {otp["otpId"]}\n\nThis OTP is valid for 5 minutes.";
                            await _emailService.SendmailAsync(loginRequest.EmailAddress, "OTP Verification", emailBody);
                        }
                        else
                        {
                            return BadRequest(new { Message = ResponseConstants.PasswordRequired });
                        }
                        HttpContext.Session.SetString("Token", token);
                        return Ok(new { Status = true, UserId = user.Data.UserId, message = ResponseConstants.FillOtpToVerify, OTPID = otp["otpId"] });
                    }
                    else
                    {
                        // Handle failed login attempt
                        user.Data.FailedLoginAttempts++;
                        user.Data.LastFailedLoginAttempt = DateTime.UtcNow;
                        Guid userId = user.Data.UserGuid;
                        await _userService.UpdateUserONLogin(user.Data);
                        if (user.Data.FailedLoginAttempts >= 5)
                        {
                            user.Data.IsLocked = true;
                            await _userService.UpdateUserONLogin(user.Data);
                        }
                        // Passwords do not match
                        return Unauthorized(new { Message = ResponseConstants.PasswordRequired, Status = false });
                    }
                }
                else
                {
                    return BadRequest(new { Status = false, Message = user.Message });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = "error: " + ex.Message, });
            }
        }

        /// <summary>
        /// This method for verify OTP during login process
        /// </summary>
        /// <param name="Otp"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("LoginVerify")]
        public async Task<IActionResult> LoginVerify([FromForm] string Otp, [FromForm] int userId)
        {
            try
            {


                if (Otp != null)
                {
                    var result = await _userService.GetLoginLogByUserIdAsync(userId);
                    //var validationResult = CommonMethods.ValidateOtp(Otp, HttpContext);

                    if (result.OTPID == null || result?.OTP == null || result?.LoginTime == null)
                    {
                        return BadRequest(new { Message = ResponseConstants.ExpireOTP });
                    }

                    var otpGeneratedTime = result.LoginTime;
                    if (DateTime.UtcNow > otpGeneratedTime.AddMinutes(5))
                    {
                        return BadRequest(new { Message = ResponseConstants.ExpiredOTP });
                    }

                    if (result.OTP == Convert.ToInt32(Otp))
                    {
                        return Ok(new { Message = ResponseConstants.ValidOTP, Token = result.Token });
                    }
                    else
                    {
                        return BadRequest(new { Message = ResponseConstants.InValidOTP });
                    }
                }
                else
                {
                    // Passwords do not match
                    return Unauthorized(new { Message = ResponseConstants.PasswordRequired });
                }
            }
            catch (Exception ex)
            {
               //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message, status = "false hai" });
            }

        }

        /// <summary>
        /// This mwthod for register user by referral link
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAuthDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = ResponseConstants.InvalidUserdata, Success = false });
                }
                user.UserGuid = Guid.NewGuid();
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.TPassword = BCrypt.Net.BCrypt.HashPassword(user.TPassword);
                user.UserCode = await _userService.GenerateUserCodeAsync();
                var result = await _userService.AddUserAsync(user);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ResponseConstants.PTA });
                }
                else if (result.Success)
                {
                    return Ok(new { Message = result.Message, Status = result.Success, data = result.Data });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = result.Message, Status = false });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// This method for verify email during register
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// 
        //[HttpGet("verify-email-by-email/{email}")]
        [HttpGet("verify-email-by-email")]
        public async Task<IActionResult> VerifyEmailbyEmail(string email)
        {
            if (email == null)
            {
                return BadRequest(new { Message = ResponseConstants.InvalidUserdata, Success = false });
            }

            ServiceResultDTO<bool> result = await _userService.CheckEmailExists(email);
            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message, Success = false });
            }
            var otp = CommonMethods.GenerateOtpForEmailVerify(_httpContextAccessor.HttpContext);
            var log = new LoginLog
            {
                UserId = 0,
                IPAddress = GetClientIpAddress(),
                UserAgent = GetClientUserAgent(),
                LoginTime = DateTime.UtcNow,
                OTP = (int)Convert.ToUInt32(otp["otp"]),
                OTPID = otp["otpId"],
                Token = email,
            };
            await _userService.AddLoginLogAsync(log);
            if (otp != null)
            {
                // Construct the email body with string interpolation
                string emailBody = await _emailService.GenerateEmailVerificationEmailBody(email, otp["otp"], otp["otpId"]);
                //string emailBody = $"Dear {user.Data.Name}\n\n,\n\nYour OTP for login is: {otp["otp"]} for OTP-ID: {otp["otpId"]}\n\nThis OTP is valid for 5 minutes.";
                await _emailService.SendmailAsync(email, "Email Verification", emailBody);
                return Ok(new { Status = true, Message = ResponseConstants.OTPEmailCheck });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = ResponseConstants.NotSendOTP, Status = false });
            }
        }

        /// <summary>
        /// This method for verify email by otp during register
        /// </summary>
        /// <param name="Otp"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        //[HttpGet("verify-email/{email}/{Otp}")]
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string Otp, string email)
        {
            if (Otp != null)
            {
                var result = await _userService.GetLoginLogByUserIdAsync(email);
                //var validationResult = CommonMethods.ValidateOtp(Otp, HttpContext);

                if (result.OTPID == null || result?.OTP == null || result?.LoginTime == null)
                {
                    return BadRequest(new { Message = ResponseConstants.ExpireOTP });
                }

                var otpGeneratedTime = result.LoginTime;
                if (DateTime.UtcNow > otpGeneratedTime.AddMinutes(5))
                {
                    return BadRequest(new { Message = ResponseConstants.ExpiredOTP });
                }

                if (result.OTP == Convert.ToInt32(Otp))
                {
                    return Ok(new { Message = ResponseConstants.ValidOTP, Token = result.Token });
                }
                else
                {
                    return BadRequest(new { Message = ResponseConstants.InValidOTP });
                }
            }
            else
            {
                // Passwords do not match
                return Unauthorized(new { Message = ResponseConstants.PasswordRequired });
            }
        }

        /// <summary>
        /// This method for change password for user.
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns></returns>

        //[Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO changePasswordDto)
        {
            try
            {
                changePasswordDto.NewPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
                var user = await _userService.ChangePasswordAsync(changePasswordDto);
                if (user.Success)
                {
                    return Ok(user);
                }
                else
                {
                    // Passwords do not match   
                    return Unauthorized(new { Message = ResponseConstants.OldPassNoMatch });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// This method for forgot password for user.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string Email)
        {
            try
            {
                var data = await _userService.ForgotPassword(Email);
                if (data.Success)
                {
                    string emailBody = "";
                    var token = CommonMethods.EncryptPassword(data.Data.UserId.ToString());
                    // Construct the email body with string interpolation

                    string resetPasswordUrl = $"https://{ResponseConstants.commonUrl}/page/account/resetpassword?token={token}";
                    emailBody = await _emailService.GeneratePasswordResetEmailBody(data.Data.Name, resetPasswordUrl);

                    await _emailService.SendmailAsync(data.Data.EmailAddress, "Reset Password", emailBody);
                }
                else
                {
                    return BadRequest(new { Message = "Email not Found. Please enter registered email Address.", Status = false });
                }
                return Ok(new { status = true, message = ResponseConstants.EmailCheckResetLink });
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// This method for reset password by link user.
        /// </summary>
        /// <param name="resetPasswordRequestDTO"></param>
        /// <returns></returns>

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            try
            {
                var id = CommonMethods.DecryptPassword(resetPasswordRequestDTO.token);
                resetPasswordRequestDTO.UserId = int.Parse(id);
                resetPasswordRequestDTO.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequestDTO.Password);
                var user = await _userService.ResetPasswordAsync(resetPasswordRequestDTO);
                if (user.Success)
                {
                    return Ok(user);
                }
                else
                {
                    // Passwords do not match   
                    return Unauthorized(new { Message = ResponseConstants.ResetPasswordFailed, Success = true });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        #endregion UserAuth

        #region AdminAuth

        /// <summary>
        /// This method for admin login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                var user = await _userService.AdminLogin(loginRequest);
                if (user.Data != null)
                {
                    if (user.Data.IsLocked)
                    {
                        return Unauthorized(new { Message = ResponseConstants.Multiplelogin });
                    }
                    if (user.Data.UserTypeId == 2)
                    {
                        return Unauthorized(new { Message = ResponseConstants.NoAccess, Status = false });
                    }
                    if (user.Success && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Data.Password))
                    {
                        var token = _tokenService.GenerateToken(user.Data);
                        HttpContext.Session.SetString("token", token);
                        var otp = CommonMethods.GenerateOtpForLogin(_httpContextAccessor.HttpContext);

                        var log = new LoginLog
                        {
                            UserId = user.Data.UserId,
                            IPAddress = GetClientIpAddress(),
                            UserAgent = GetClientUserAgent(),
                            LoginTime = DateTime.UtcNow,
                            OTP = (int)Convert.ToUInt32(otp["otp"]),
                            OTPID = otp["otpId"],
                            Token = token,
                        };
                        await _userService.AddLoginLogAsync(log);

                        if (token != null)
                        {
                            user.Data.FailedLoginAttempts = 0;
                            Guid userId = user.Data.UserGuid;
                            await _userService.UpdateAdminONLogin(user.Data);
                            // Construct the email body with string interpolation
                            string otps = otp["otp"];
                            string emailBody = await _emailService.GenerateEmailBody(user.Data.Name, otp["otp"], otp["otpId"]);
                            //string emailBody = $"Dear {user.Data.Name}\n\n,\n\nYour OTP for login is: {otp["otp"]} for OTP-ID: {otp["otpId"]}\n\nThis OTP is valid for 5 minutes.";
                            await _emailService.SendmailAsync(loginRequest.EmailAddress, "OTP Verification", emailBody);
                        }
                        else
                        {
                            return Unauthorized(new { Message = ResponseConstants.PasswordRequired });
                        }
                        string maskedEmail = MaskEmail(loginRequest.EmailAddress);
                        HttpContext.Session.SetString("Token", token);
                        return Ok(new { Status = true, UserId = user.Data.UserId, message = ResponseConstants.FillOtpToVerify, OTPID = otp["otpId"], maskedEmail = maskedEmail });
                    }
                    else
                    {
                        // Handle failed login attempt
                        user.Data.FailedLoginAttempts++;
                        user.Data.LastFailedLoginAttempt = DateTime.UtcNow;
                        Guid userId = user.Data.UserGuid;
                        await _userService.UpdateAdminONLogin(user.Data);
                        if (user.Data.FailedLoginAttempts >= 5)
                        {
                            user.Data.IsLocked = true;
                            await _userService.UpdateAdminONLogin(user.Data);
                        }
                        // Passwords do not match
                        return Unauthorized(new { Message = ResponseConstants.PasswordRequired, Status = false });
                    }
                }
                else
                {
                    return BadRequest(new { Status = false, Message = user.Message });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// This method for forgot password for admin.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ForgotPasswordforAdmin([FromQuery] string Email)
        {
            try
            {
                var data = await _userService.ForgotPasswords(Email);
                if (data.Data != null && data.Data.UserTypeId==1)
                {
                    string emailBody = "";
                    var token = CommonMethods.EncryptPassword(data.Data.UserId.ToString());



                    string resetPasswordUrl = $"https://admin.devmoral.com/reset-password?token={token}";
                    emailBody = await _emailService.GeneratePasswordResetEmailBody(data.Data.Name, resetPasswordUrl);
                    await _emailService.SendmailAsync(data.Data.EmailAddress, "Reset Password", emailBody);


                    return Ok(new { status = true, message = ResponseConstants.EmailCheckResetLink });

                }
                else if (data.Data != null && data.Data.UserTypeId != 1)
                {
                    return BadRequest(new { Message = ResponseConstants.NoAccessToResetPass });
                }
                else
                {
                    return BadRequest(new { Message = ResponseConstants.InvalidEmail });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }

        }

        /// <summary>
        /// This method for reset password by link admin
        /// </summary>
        /// <param name="resetPasswordRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> AdminResetPassword([FromBody] ResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            try
            {
                var id = CommonMethods.DecryptPassword(resetPasswordRequestDTO.token);
                resetPasswordRequestDTO.UserId = int.Parse(id);
                resetPasswordRequestDTO.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequestDTO.Password);
                var user = await _userService.AdminResetPasswordAsync(resetPasswordRequestDTO);
                if (user.Success)
                {
                    return Ok(user);
                }
                else
                {
                    // Passwords do not match   
                    return Unauthorized(new { Message = ResponseConstants.ResetPasswordFailed, Success = true });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// This method for create email to mask email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains('@'))
            {
                return email; // Invalid email, return as is.
            }
            // Email ko user aur domain parts me split karein.
            var parts = email.Split('@');
            var user = parts[0];
            var domain = parts[1];

            // User aur domain parts ko mask karein.
            var maskedUser = user[0] + "****" + user[^1];
            var maskedDomain = domain[0] + "****" + domain[^1];

            // Masked email ko return karein.
            return $"{maskedUser}@{maskedDomain}";
        }

        /// <summary>
        /// This method for reset password for admin staff and send their password to staff emails.
        /// </summary>
        /// <param name="resetPasswordRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> AdminStaffResetPassword([FromBody] AdminResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            try
            {
                string emailBody = "";
                var UserData = await _userService.GetAdminByIdAsync(resetPasswordRequestDTO.UserId);
                emailBody = await _emailService.GenerateAdminStaffEmailBody(UserData.Name, "https://admin.devmoral.com/login", resetPasswordRequestDTO.Password, UserData.EmailAddress);
                await _emailService.SendmailAsync(UserData.EmailAddress, "Admin Staff Reset Password", emailBody);
                resetPasswordRequestDTO.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequestDTO.Password);
                var user = await _userService.AdminStaffResetPasswordAsync(resetPasswordRequestDTO);
                if (user.Success)
                {
                    return Ok(user);
                }
                else
                {

                    // Passwords do not match   
                    return Unauthorized(new { Message = ResponseConstants.ResetPasswordFailed, Success = true });
                }
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        #endregion AdminAuth

        #region Commons
        /// <summary>
        /// This method for get user IP Address
        /// </summary>
        /// <returns></returns>
        private string GetClientIpAddress()
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;
            return ipAddress?.ToString() ?? "Unknown";
        }

        /// <summary>
        /// This method for get user browser details; like chrome
        /// </summary>
        /// <returns></returns>
        private string GetClientUserAgent()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
        }

        /// <summary>
        /// This method for store login log details. how many time user login with which OTP and tokens?
        /// </summary>
        /// <param name="loginLog"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> AddLoginLog([FromBody] LoginLog loginLog)
        {
            try
            {
                var result = await _userService.AddLoginLogAsync(loginLog);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error adding log." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// This method for get login log detais by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpPost("[Action]")]
        public async Task<IActionResult> GetLoginLogByUserIdAsync(int userId)
        {
            try
            {
                var result = await _userService.GetLoginLogByUserIdAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }



        [HttpPost("[Action]")]
        public async Task<IActionResult> ContactUs([FromBody] ContactUsDTO contactUs)
        {
            try
            {
                string emailBody = "";

                emailBody = await _emailService.GenerateContactUsEmailBody(contactUs.FirstName, contactUs.LastName, contactUs.PhoneNumber, contactUs.Email, contactUs.Message);
                await _emailService.SendmailAsync("sahilmaurya7488@gmail.com", "New Contact Us Message", emailBody);
                return Ok(new { Message = "Email Send Succesfully" });

            }
            catch (Exception ex)
            {
                //await _kycService.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, ControllerContext.ActionDescriptor.ActionName, ex.Message, ex.StackTrace);
                return BadRequest(new { Message = ex.Message });
            }
        }




        #endregion Commons
        
    }
}
