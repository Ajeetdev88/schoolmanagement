using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MLMProject.Application.DTOs;
using MLMProject.Domain.Entities;
using OfficeOpenXml;

namespace MLMProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ExcelUserRepository _userRepository;

        private readonly string _filePath;

        public UserController()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Users.xlsx");
        }

        // GET: api/Users
        [HttpGet]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(_filePath)))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        return NotFound("No data found in the Excel file.");
                    }

                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    var users = new List<UserAuthDTO>();

                    for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
                    {
                        var user = new UserAuthDTO
                        {
                            UserId = int.Parse(worksheet.Cells[row, 1].Text),
                            UserGuid = Guid.Parse(worksheet.Cells[row, 2].Text),
                            UserCode = worksheet.Cells[row, 3].Text,
                            UserName = worksheet.Cells[row, 4].Text,
                            Name = worksheet.Cells[row, 5].Text,
                            DOB = DateTime.Parse(worksheet.Cells[row, 6].Text),
                            EmailAddress = worksheet.Cells[row, 7].Text,
                            PhoneNumber = worksheet.Cells[row, 8].Text,
                            UserTypeId = int.Parse(worksheet.Cells[row, 9].Text),
                            Password = worksheet.Cells[row, 10].Text,
                            TPassword = worksheet.Cells[row, 11].Text,
                            CreatedDate = DateTime.Parse(worksheet.Cells[row, 12].Text),
                            CreatedBy = int.Parse(worksheet.Cells[row, 13].Text),
                            IsDeleted = bool.Parse(worksheet.Cells[row, 14].Text),
                            IsLocked = bool.Parse(worksheet.Cells[row, 15].Text),
                            IsActive = bool.Parse(worksheet.Cells[row, 16].Text),
                            FailedLoginAttempts = int.Parse(worksheet.Cells[row, 17].Text),
                            LastFailedLoginAttempt = DateTime.Parse(worksheet.Cells[row, 18].Text)
                        };

                        users.Add(user);
                    }

                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult AddUser([FromBody] UserAuthDTO user)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(_filePath)))
                {
                    ExcelWorksheet worksheet;
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        worksheet = package.Workbook.Worksheets.Add("Users");
                        worksheet.Cells[1, 1].Value = "UserId";
                        worksheet.Cells[1, 2].Value = "UserGuid";
                        worksheet.Cells[1, 3].Value = "UserCode";
                        worksheet.Cells[1, 4].Value = "UserName";
                        worksheet.Cells[1, 5].Value = "Name";
                        worksheet.Cells[1, 6].Value = "DOB";
                        worksheet.Cells[1, 7].Value = "EmailAddress";
                        worksheet.Cells[1, 8].Value = "PhoneNumber";
                        worksheet.Cells[1, 9].Value = "UserTypeId";
                        worksheet.Cells[1, 10].Value = "Password";
                        worksheet.Cells[1, 11].Value = "TPassword";
                        worksheet.Cells[1, 12].Value = "CreatedDate";
                        worksheet.Cells[1, 13].Value = "CreatedBy";
                        worksheet.Cells[1, 14].Value = "IsDeleted";
                        worksheet.Cells[1, 15].Value = "IsLocked";
                        worksheet.Cells[1, 16].Value = "IsActive";
                        worksheet.Cells[1, 17].Value = "FailedLoginAttempts";
                        worksheet.Cells[1, 18].Value = "LastFailedLoginAttempt";
                    }
                    else
                    {
                        worksheet = package.Workbook.Worksheets[0];
                    }

                    var rowCount = worksheet.Dimension?.Rows ?? 1;
                    worksheet.Cells[rowCount + 1, 1].Value = user.UserId;
                    worksheet.Cells[rowCount + 1, 2].Value = user.UserGuid.ToString();
                    worksheet.Cells[rowCount + 1, 3].Value = user.UserCode;
                    worksheet.Cells[rowCount + 1, 4].Value = user.UserName;
                    worksheet.Cells[rowCount + 1, 5].Value = user.Name;
                    worksheet.Cells[rowCount + 1, 6].Value = user.DOB.ToString("yyyy-MM-dd");
                    worksheet.Cells[rowCount + 1, 7].Value = user.EmailAddress;
                    worksheet.Cells[rowCount + 1, 8].Value = user.PhoneNumber;
                    worksheet.Cells[rowCount + 1, 9].Value = user.UserTypeId;
                    worksheet.Cells[rowCount + 1, 10].Value = user.Password;
                    worksheet.Cells[rowCount + 1, 11].Value = user.TPassword;
                    worksheet.Cells[rowCount + 1, 12].Value = user.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[rowCount + 1, 13].Value = user.CreatedBy;
                    worksheet.Cells[rowCount + 1, 14].Value = user.IsDeleted;
                    worksheet.Cells[rowCount + 1, 15].Value = user.IsLocked;
                    worksheet.Cells[rowCount + 1, 16].Value = user.IsActive;
                    worksheet.Cells[rowCount + 1, 17].Value = user.FailedLoginAttempts;
                    worksheet.Cells[rowCount + 1, 18].Value = user.LastFailedLoginAttempt.ToString("yyyy-MM-dd HH:mm:ss");
                
                   package.Save();
                }

                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Users
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserAuthDTO user)
        {
            try
            {
                if (!System.IO.File.Exists(_filePath))
                {
                    return NotFound("Excel file not found.");
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(_filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (int.Parse(worksheet.Cells[row, 1].Text) == user.UserId)
                        {
                            worksheet.Cells[row, 4].Value = user.UserName;
                            worksheet.Cells[row, 5].Value = user.Name;
                            worksheet.Cells[row, 6].Value = user.DOB.ToString("yyyy-MM-dd");
                            worksheet.Cells[row, 7].Value = user.EmailAddress;
                            worksheet.Cells[row, 8].Value = user.PhoneNumber;
                            worksheet.Cells[row, 16].Value = user.IsActive;

                            package.Save();
                            return Ok("User updated successfully.");
                        }
                    }

                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (!System.IO.File.Exists(_filePath))
                {
                    return NotFound("Excel file not found.");
                }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(_filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (int.Parse(worksheet.Cells[row, 1].Text) == id)
                        {
                            worksheet.DeleteRow(row);
                            package.Save();
                            return Ok("User deleted successfully.");
                        }
                    }
                }

                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

