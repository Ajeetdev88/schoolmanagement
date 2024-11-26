using AutoMapper;
using MLMProject.Application.DTOs;
using MLMProject.Application.Interfaces;
using MLMProject.Application.IRepository;
using MLMProject.Domain.Entities;

namespace MLMProject.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        #region Admin
        public async Task<ServiceResultDTO<AdminAuthDTO>> AdminLogin(LoginRequestDTO loginDto)
        {
            // Perform validation
            if (string.IsNullOrWhiteSpace(loginDto.EmailAddress) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return new ServiceResultDTO<AdminAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.CredentialsRequired
                };
            }

            // Map DTO to entity
            var user = _mapper.Map<Domain.Entities.LoginRequest>(loginDto);

            // Authenticate user
            var authenticatedUser = await _userRepository.AdminLogin(user);
            if (authenticatedUser == null)
            {
                return new ServiceResultDTO<AdminAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.InvalidCredentials
                };
            }
            if (authenticatedUser.IsActive == false)
            {
                return new ServiceResultDTO<AdminAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.AccountNotActive
                };
            }

            if (authenticatedUser.IsDeleted == true)
            {
                return new ServiceResultDTO<AdminAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.AccountDeleted
                };
            }

            // Map entity back to DTO
            var userAuthDto = _mapper.Map<AdminAuthDTO>(authenticatedUser);

            return new ServiceResultDTO<AdminAuthDTO>
            {
                Success = true,
                Data = userAuthDto
            };
        }
        public async Task<ServiceResultDTO<AdminAuthDTO>> AddAdminStaffAsync(AdminAuthDTO user)
        {
            ServiceResultDTO<AdminAuthDTO> serviceResultDTO = new ServiceResultDTO<AdminAuthDTO>();
            var userAuth = _mapper.Map<AdminAuth>(user);

            var userdata = await _userRepository.AddAdminStaffAsync(userAuth);
            serviceResultDTO.Success = userdata.Success;
            serviceResultDTO.Message = userdata.Message;
            serviceResultDTO.Data = _mapper.Map<AdminAuthDTO>(userdata.Data);
            return serviceResultDTO;
        }
        public async Task<AdminAuthDTO> UpdateAdmin(AdminAuthDTO user)
        {
            var userAuth = _mapper.Map<AdminAuth>(user);
            var userdata = await _userRepository.UpdateAdminAsync(userAuth);
            return _mapper.Map<AdminAuthDTO>(userdata);
        }
        public async Task<AdminAuthDTO> UpdateAdminONLogin(AdminAuthDTO user)
        {
            var userAuth = _mapper.Map<AdminAuth>(user);
            var userdata = await _userRepository.UpdateAdminONLogin(userAuth);
            return _mapper.Map<AdminAuthDTO>(userdata);
        }
        public async Task<IEnumerable<AdminAuthDTO>> GetAllAdminAsync()
        {
            var res = await _userRepository.GetAllAdminAsync();
            var res1 = _mapper.Map<IEnumerable<AdminAuthDTO>>(res);
            return res1;
        }
        public async Task<AdminAuthDTO> GetAdminByIdAsync(int id)
        {
            var userdata = await _userRepository.GetAdminByIdAsync(id);
            return _mapper.Map<AdminAuthDTO>(userdata);
        }
        public async Task<string> GenerateAdminCodeAsync()
        {
            // Get the total number of users in the database
            var userCount = await _userRepository.GetAdminCountAsync();

            // Increment the user count to get the next serial number
            int serialNumber = userCount + 1;

            // Create UserCode with "EU" prefix and the serial number padded to 4 digits
            return $"EU{serialNumber:D4}";
        }
        public async Task<ServiceResultDTO<AdminAuthDTO>> ForgotPasswords(string Email)
        {
            // Authenticate user
            var authenticatedUser = await _userRepository.ForgotPasswords(Email);
            if (authenticatedUser == null)
            {
                return new ServiceResultDTO<AdminAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.InvalidEmail
                };
            }
            else
            {
                // Map entity back to DTO
                var userAuthDto = _mapper.Map<AdminAuthDTO>(authenticatedUser);

                return new ServiceResultDTO<AdminAuthDTO>
                {
                    Success = true,
                    Message = ResponseConstants.FPEmailSent,
                    Data = userAuthDto
                };
            }
        }
        public async Task<ServiceResultDTO<bool>> AdminResetPasswordAsync(ResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            // Perform validation
            if (resetPasswordRequestDTO.UserId == null || string.IsNullOrWhiteSpace(resetPasswordRequestDTO.Password))
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = ResponseConstants.ResetPasswordFailed;
            }

            // Map DTO to entity
            var resetpasswords = _mapper.Map<Domain.Entities.ResetPasswordRequest>(resetPasswordRequestDTO);

            // Authenticate user
            serviceResultDTO = await _userRepository.AdminResetPassword(resetpasswords);
            if (serviceResultDTO == null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = ResponseConstants.ResetPasswordFailed;

            }
            return serviceResultDTO;
            // Map entity back to DTO
        }
        public async Task<ServiceResultDTO<bool>> AdminStaffResetPasswordAsync(AdminResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            // Perform validation
            if (resetPasswordRequestDTO.UserId == null || string.IsNullOrWhiteSpace(resetPasswordRequestDTO.Password))
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = ResponseConstants.ResetPasswordFailed;
            }

            // Map DTO to entity
            var resetpasswords = _mapper.Map<Domain.Entities.AdminResetPasswordRequest>(resetPasswordRequestDTO);

            // Authenticate user
            serviceResultDTO = await _userRepository.AdminStaffResetPassword(resetpasswords);
            if (serviceResultDTO == null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = ResponseConstants.ResetPasswordFailed;

            }
            return serviceResultDTO;
            // Map entity back to DTO
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userdata = await _userRepository.DeleteUserAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> ActiveUserAsync(int id)
        {
            var userdata = await _userRepository.ActiveUserAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> DeactiveUserAsync(int id)
        {
            var userdata = await _userRepository.DeactiveUserAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> DeleteAdminAsync(int id)
        {
            var userdata = await _userRepository.DeleteAdminAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> ActiveAdminAsync(int id)
        {
            var userdata = await _userRepository.ActiveAdminAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> DeactiveAdminAsync(int id)
        {
            var userdata = await _userRepository.DeactiveAdminAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> LockUserAsync(int id)
        {
            var userdata = await _userRepository.LockUserAsync(id);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<bool> UnlockUserAsync(int id)
        {
            var userdata = await _userRepository.UnlockUserAsync(id);
            return _mapper.Map<bool>(userdata);
        }
      
        public async Task<PrivacyPolicyDTO> AddPrivacyPolicyAsync(PrivacyPolicy privacyPolicy)
        {
            var logdata = await _userRepository.AddPrivacyPolicyAsync(privacyPolicy);
            return _mapper.Map<PrivacyPolicyDTO>(logdata);
        }
        public async Task<SessionEntityDTO> AddSessionAsync(SessionEntity sessionEntity)
        {
            var logdata = await _userRepository.AddSessionAsync(sessionEntity);
            return _mapper.Map<SessionEntityDTO>(logdata);
        }

        #endregion Admin

        #region User
        public async Task<ServiceResultDTO<UserAuthDTO>> Login(LoginRequestDTO loginDto)
        {
            // Perform validation
            if (string.IsNullOrWhiteSpace(loginDto.EmailAddress) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return new ServiceResultDTO<UserAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.CredentialsRequired
                };
            }

            // Map DTO to entity
            var user = _mapper.Map<Domain.Entities.LoginRequest>(loginDto);

            // Authenticate user
            var authenticatedUser = await _userRepository.Login(user);
            if (authenticatedUser == null)
            {
                return new ServiceResultDTO<UserAuthDTO>
                {
                    Success = false,
                    Message =  ResponseConstants.InvalidCredentials
                };
            }
            if (authenticatedUser.IsActive == false)
            {
                return new ServiceResultDTO<UserAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.AccountNotActive
                };
            }

            if (authenticatedUser.IsDeleted == true)
            {
                return new ServiceResultDTO<UserAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.AccountDeleted
                };
            }


            // Map entity back to DTO
            var userAuthDto = _mapper.Map<UserAuthDTO>(authenticatedUser);

            return new ServiceResultDTO<UserAuthDTO>
            {
                Success = true,
                Data = userAuthDto
            };
        }
        public async Task<ServiceResultDTO<UserAuthDTO>> AddUserAsync(UserAuthDTO user)
        {
            ServiceResultDTO<UserAuthDTO> serviceResultDTO = new ServiceResultDTO<UserAuthDTO>();
            var userAuth = _mapper.Map<UserAuth>(user);
            
            var userdata =  await _userRepository.AddUserAsync(userAuth);
            serviceResultDTO.Success =userdata.Success;
            serviceResultDTO.Message = userdata.Message;
            serviceResultDTO.Data= _mapper.Map<UserAuthDTO>(userdata.Data);
            return serviceResultDTO;
        }
        public async Task<ServiceResultDTO<UserAuthDTO>> AddUserStaffAsync(UserAuthDTO user)
        {
            ServiceResultDTO<UserAuthDTO> serviceResultDTO = new ServiceResultDTO<UserAuthDTO>();
            var userAuth = _mapper.Map<UserAuth>(user);

            var userdata = await _userRepository.AddUserStaffAsync(userAuth);
            serviceResultDTO.Success = userdata.Success;
            serviceResultDTO.Message = userdata.Message;
            serviceResultDTO.Data = _mapper.Map<UserAuthDTO>(userdata.Data);
            return serviceResultDTO;
        }
        public async Task<UserAuthDTO> UpdateUser(UserAuthDTO user)
        {
            var userAuth = _mapper.Map<UserAuth>(user);
            var userdata = await _userRepository.UpdateUserAsync(userAuth);
            return _mapper.Map<UserAuthDTO>(userdata);
        }
        public async Task<UserAuthDTO> UpdateUserONLogin(UserAuthDTO user)
        {
            var userAuth = _mapper.Map<UserAuth>(user);
            var userdata = await _userRepository.UpdateUserONLogin(userAuth);
            return _mapper.Map<UserAuthDTO>(userdata);
        }
        public async Task<IEnumerable<UserAuthDTO>> GetAllUsersAsync()
        {
          var res = await _userRepository.GetAllUsersAsync();
            var res1 = _mapper.Map<IEnumerable<UserAuthDTO>>(res);
            return res1;
        }
        public async Task<IEnumerable<UserTypeDTO>> GetAllUserTypeAsync()
        {
            var res = await _userRepository.GetAllUserTypeAsync();
            var res1 = _mapper.Map<IEnumerable<UserTypeDTO>>(res);
            return res1;
        }
        public async Task<UserAuthDTO> GetUserByIdAsync(int id)
        {
          var userdata= await  _userRepository.GetUserByIdAsync(id);
            return  _mapper.Map<UserAuthDTO>(userdata);
        }
        public async Task<string> GenerateUserCodeAsync()
        {
            // Get the total number of users in the database
            var userCount = await _userRepository.GetUserCountAsync();

            // Increment the user count to get the next serial number
            int serialNumber = userCount + 1;

            // Create UserCode with "EU" prefix and the serial number padded to 4 digits
            return $"EU{serialNumber:D4}";
        }
        public async Task<LoginLogDTO> AddLoginLogAsync(LoginLog loginLog)
        {
            var logdata = await _userRepository.AddLoginLogAsync(loginLog);
            return _mapper.Map<LoginLogDTO>(logdata);
        }
        public async Task<LoginLogDTO> GetLoginLogByUserIdAsync(int userId)
        {
            var userdata = await _userRepository.GetLoginLogByUserIdAsync(userId);
            return _mapper.Map<LoginLogDTO>(userdata);
        }
        public async Task<LoginLogDTO> GetLoginLogByUserIdAsync(string email)
        {
            var userdata = await _userRepository.GetLoginLogByUserIdAsync(email);
            return _mapper.Map<LoginLogDTO>(userdata);
        }
        public async Task<PrivacyPolicyDTO> GetPrivacyPolicyAsync()
        {
            var logdata = await _userRepository.GetPrivacyPolicyAsync();
            return _mapper.Map<PrivacyPolicyDTO>(logdata);
        }

      
        public async Task<SessionEntityDTO> GetSessionAsync()
        {
            var logdata = await _userRepository.GetSessionAsync();
            return _mapper.Map<SessionEntityDTO>(logdata);
        }
        public async Task<ServiceResultDTO<UserAuthDTO>> ForgotPassword(string Email)
        {
            // Authenticate user
            var authenticatedUser = await _userRepository.ForgotPassword(Email);
            if (authenticatedUser == null)
            {
                return new ServiceResultDTO<UserAuthDTO>
                {
                    Success = false,
                    Message = ResponseConstants.InvalidEmail
                };
            }
            else
            {
                // Map entity back to DTO
                var userAuthDto = _mapper.Map<UserAuthDTO>(authenticatedUser);

                return new ServiceResultDTO<UserAuthDTO>
                {
                    Success = true,
                    Message = ResponseConstants.FPEmailSent,
                    Data = userAuthDto
                };
            }
        }
        public async Task<ServiceResultDTO<ChangePasswordRequestDTO>> ChangePasswordAsync(ChangePasswordRequestDTO changePasswordRequest)
        {
            // Perform validation
            if (string.IsNullOrWhiteSpace(changePasswordRequest.Email) || string.IsNullOrWhiteSpace(changePasswordRequest.NewPassword) || string.IsNullOrWhiteSpace(changePasswordRequest.CurrentPassword))
            {
                return new ServiceResultDTO<ChangePasswordRequestDTO>
                {
                    Success = false,
                    Message = ResponseConstants.PasswordRequired
                };
            }

            // Map DTO to entity
            var changepasswords = _mapper.Map<ChangePasswordRequest>(changePasswordRequest);

            // Authenticate user
            var authenticatedUser = await _userRepository.ChangePassword(changepasswords);
            if (authenticatedUser.Success == false)
            {
                return new ServiceResultDTO<ChangePasswordRequestDTO>
                {
                    Success = false,
                    Message = ResponseConstants.PasswordRequired
                };
            }
            // Map entity back to DTO

            return new ServiceResultDTO<ChangePasswordRequestDTO>
            {
                Success = true,
                Message = ResponseConstants.ChangePasswordSuccess
            };
        }
        public async Task<ServiceResultDTO<bool>> ResetPasswordAsync(ResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            // Perform validation
            if (resetPasswordRequestDTO.UserId ==null || string.IsNullOrWhiteSpace(resetPasswordRequestDTO.Password))
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = ResponseConstants.ResetPasswordFailed;
            }

            // Map DTO to entity
            var resetpasswords = _mapper.Map<Domain.Entities.ResetPasswordRequest>(resetPasswordRequestDTO);

            // Authenticate user
             serviceResultDTO = await _userRepository.ResetPassword(resetpasswords);
            if (serviceResultDTO == null)
            {
                serviceResultDTO.Success= false;
                serviceResultDTO.Message = ResponseConstants.ResetPasswordFailed;
               
            }
         return serviceResultDTO;
            // Map entity back to DTO
        }
        public async Task<UserAuthDTO> GetSponsorDetails(Guid id)
        {
            var userdata = await _userRepository.GetSponsorDetails(id);
            return _mapper.Map<UserAuthDTO>(userdata);
        }
        public async Task<UserAuthDTO> GetSponsorDetailsbySponsorCode(string sponsorCode)
        {
            var userdata = await _userRepository.GetSponsorDetailsbySponsorCode(sponsorCode);
            return _mapper.Map<UserAuthDTO>(userdata);
        }
        public async Task<ServiceResultDTO<bool>> CheckEmailExists(string email)
        {
            var userdata = await _userRepository.CheckEmailExists(email);
            return userdata;
        }
       public async Task<ServiceResultDTO<AddressesDTO>> AddUpdateAddresses(AddressesDTO addressesDTO)
        {
            ServiceResultDTO<AddressesDTO> serviceResultDTO = new ServiceResultDTO<AddressesDTO>();
            var addresses = _mapper.Map<Addresses>(addressesDTO);
            var addressdata = await _userRepository.AddUpdateAddresses(addresses);
            var data = _mapper.Map<AddressesDTO>(addressdata.Data);
            if(data != null)
            {
                serviceResultDTO.Success = addressdata.Success;
                serviceResultDTO.Message = addressdata.Message;
                serviceResultDTO.Data = data;
            }
            else
            {
                serviceResultDTO.Success = addressdata.Success;
                serviceResultDTO.Message = addressdata.Message;
                serviceResultDTO.Data = data;
            }
            return serviceResultDTO;
        }

        public async Task<IEnumerable<AddressesDTO>> GetAllAddresses(int userId)
        {
            var addressdata = await _userRepository.GetAllAddresses(userId);
            var data = _mapper.Map<IEnumerable<AddressesDTO>>(addressdata);
            return data;
        }

        public async Task<AddressesDTO> GetShippingAddressAsync(int userId)
        {
            var addressdata = await _userRepository.GetShippingAddress(userId);
            var data = _mapper.Map<AddressesDTO>(addressdata);
            return data;
        }
        public async Task<AddressesDTO> GetAddressById(int addressId)
        {
            var addressdata = await _userRepository.GetAddressById(addressId);
            var data = _mapper.Map<AddressesDTO>(addressdata);
            return data;
        }
        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var userdata = await _userRepository.DeleteAddressAsync(addressId);
            return _mapper.Map<bool>(userdata);
        }

        public async Task<bool> SetDefaltAddressAsync(int addressId)
        {
            var userdata = await _userRepository.SetDefaltAddressAsync(addressId);
            return _mapper.Map<bool>(userdata);
        }
        public async Task<WelcomeMessageDTO> AddWelcomeMessageAsync(WelcomeMessage welcomeMessage)
        {
            var logdata = await _userRepository.AddWelcomeMessageAsync(welcomeMessage);
            return _mapper.Map<WelcomeMessageDTO>(logdata);
        }
        public async Task<WelcomeMessageDTO> GetWelcomeMessageAsync()
        {
            var logdata = await _userRepository.GetWelcomeMessageAsync();
            return _mapper.Map<WelcomeMessageDTO>(logdata);
        }

        #endregion User

    }
}







