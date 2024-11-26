using MLMProject.Domain.Entities;
using MLMProject.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MLMProject.Application.IRepository;
using MLMProject.Application.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace MLMProject.Infrastructure.Repository
{
    public class UserRepository(MLMProjectDbContext dbContext) : IUserRepository
    {
        private readonly MLMProjectDbContext _dbContext = dbContext;

        public async Task<UserAuth> Login(Domain.Entities.LoginRequest loginRequest)
        {
            UserAuth userAuth = new UserAuth();
             userAuth = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.EmailAddress == loginRequest.EmailAddress);
            return userAuth;
        }
        public async Task<AdminAuth> AdminLogin(Domain.Entities.LoginRequest loginRequest)
        {
            AdminAuth userAuth = new AdminAuth();
            userAuth = await _dbContext.AdminAuths.FirstOrDefaultAsync(u => u.EmailAddress == loginRequest.EmailAddress  || u.UserName== loginRequest.EmailAddress);
            return userAuth;
        }
        public async Task<ServiceResultDTO<UserAuth>> AddUserAsync(UserAuth user)
        {
            ServiceResultDTO<UserAuth> serviceResultDTO = new ServiceResultDTO<UserAuth>();
            var existingUser = await _dbContext.UserAuths
             .FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress);

            var contactexists = await _dbContext.UserAuths
            .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber);
            if (existingUser != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "Email already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else if (contactexists != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "Phone number already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else
            {
                await _dbContext.UserAuths.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = "Registered successfully.";
                serviceResultDTO.Data = user;
                return serviceResultDTO;
            }
        }
        public async Task<ServiceResultDTO<UserAuth>> AddUserStaffAsync(UserAuth user)
        {
            ServiceResultDTO<UserAuth> serviceResultDTO = new ServiceResultDTO<UserAuth>();
            var existingUser = await _dbContext.UserAuths
             .FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress && u.UserTypeId!=2);

            var contactexists = await _dbContext.UserAuths
            .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber && u.UserTypeId != 2);
            if (existingUser != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "Email already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else if (contactexists != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "Phone number already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else
            {
                await _dbContext.UserAuths.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = "Added successfully.";
                serviceResultDTO.Data = user;
                return serviceResultDTO;
            }
        }
        public async Task<ServiceResultDTO<AdminAuth>> AddAdminStaffAsync(AdminAuth user)
        {
            ServiceResultDTO<AdminAuth> serviceResultDTO = new ServiceResultDTO<AdminAuth>();
            var existingUser = await _dbContext.AdminAuths
             .FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress && u.UserTypeId != 2);

            var contactexists = await _dbContext.AdminAuths
            .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber && u.UserTypeId != 2);

            var usernameexists = await _dbContext.AdminAuths
            .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserTypeId != 2);

            if (existingUser != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "Email already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else if (contactexists != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "Phone number already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else if (usernameexists != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Message = "User name already exists.";
                // Email already exists
                return serviceResultDTO;
            }
            else
            {
                await _dbContext.AdminAuths.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = "Added successfully.";
                serviceResultDTO.Data = user;
                return serviceResultDTO;
            }
        }
        public async Task<UserAuth> UpdateUserONLogin(UserAuth user)
        {
            var existingUser = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.UserGuid == user.UserGuid);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();
            return existingUser;
        }
        public async Task<AdminAuth> UpdateAdminONLogin(AdminAuth user)
        {
            var existingUser = await _dbContext.AdminAuths.FirstOrDefaultAsync(u => u.UserGuid == user.UserGuid);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Admin not found");
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();
            return existingUser;
        }
        public async Task<UserAuth> UpdateUserAsync(UserAuth user)
        {
            var existingUser = await _dbContext.UserAuths
                .FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress && u.UserTypeId != 2 && u.UserId != user.UserId);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var contactExists = await _dbContext.UserAuths
                .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber && u.UserTypeId != 2 && u.UserId != user.UserId);

            if (contactExists != null)
            {
                throw new InvalidOperationException("Phone number already exists.");
            }

            var currentUser = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.UserGuid == user.UserGuid);
            if (currentUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the user details
            _dbContext.Entry(currentUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();

            return currentUser;
        }
        public async Task<AdminAuth> UpdateAdminAsync(AdminAuth user)
        {
            var existingUser = await _dbContext.AdminAuths
                .FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress && u.UserTypeId != 2 && u.UserId != user.UserId);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var contactExists = await _dbContext.AdminAuths
                .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber && u.UserTypeId != 2 && u.UserId != user.UserId);

            if (contactExists != null)
            {
                throw new InvalidOperationException("Phone number already exists.");
            }

            var usenameExists = await _dbContext.AdminAuths
               .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserTypeId != 2 && u.UserId != user.UserId);

            if (usenameExists != null)
            {
                throw new InvalidOperationException("User name already exists.");
            }



            var currentUser = await _dbContext.AdminAuths.FirstOrDefaultAsync(u => u.UserGuid == user.UserGuid);
            if (currentUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the user details
            _dbContext.Entry(currentUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();

            return currentUser;
        }
        public async Task<UserAuth> GetUserByIdAsync(int id)
        {
            return await _dbContext.UserAuths.FindAsync(id);
        }
        public async Task<AdminAuth> GetAdminByIdAsync(int id)
        {
            return await _dbContext.AdminAuths.FindAsync(id);
        }
        public async Task<IEnumerable<UserAuth>> GetAllUsersAsync()
        {
            return await _dbContext.UserAuths
                .Where(user => user.IsDeleted == false) // Filter by IsDeleted column
                .OrderByDescending(user => user.UserId)
                .ToListAsync();
        }
        public async Task<IEnumerable<AdminAuth>> GetAllAdminAsync()
        {
            return await _dbContext.AdminAuths
                .Where(user => user.IsDeleted == false) // Filter by IsDeleted column
                .OrderByDescending(user => user.UserId)
                .ToListAsync();
        }
        public async Task<IEnumerable<UserTypeEntity>> GetAllUserTypeAsync()
        {
            return await _dbContext.UserTypes.ToListAsync();
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.UserAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsDeleted = true; // Mark as deleted
            _dbContext.UserAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActiveUserAsync(int userId)
        {
            var user = await _dbContext.UserAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false; // Mark as deleted
            _dbContext.UserAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeactiveUserAsync(int userId)
        {
            var user = await _dbContext.UserAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = true; // Mark as deleted
            _dbContext.UserAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAdminAsync(int userId)
        {
            var user = await _dbContext.AdminAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsDeleted = true; // Mark as deleted
            _dbContext.AdminAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActiveAdminAsync(int userId)
        {
            var user = await _dbContext.AdminAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false; // Mark as deleted
            _dbContext.AdminAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeactiveAdminAsync(int userId)
        {
            var user = await _dbContext.AdminAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = true; // Mark as deleted
            _dbContext.AdminAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> LockUserAsync(int userId)
        {
            var user = await _dbContext.UserAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsLocked = true; // Mark as deleted
            user.FailedLoginAttempts = 5;
            _dbContext.UserAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnlockUserAsync(int userId)
        {
            var user = await _dbContext.UserAuths.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.IsLocked = false; // Mark as deleted
            user.FailedLoginAttempts = 0;
            _dbContext.UserAuths.Update(user); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> GetUserCountAsync()
        {
            return await _dbContext.UserAuths.CountAsync();
        }
        public async Task<int> GetAdminCountAsync()
        {
            return await _dbContext.AdminAuths.CountAsync();
        }
        public async Task<LoginLog> AddLoginLogAsync(LoginLog loginLog)
        {
            await _dbContext.LoginLogs.AddAsync(loginLog);
            await _dbContext.SaveChangesAsync();
            return loginLog;
        }
        public async Task<PrivacyPolicy> AddPrivacyPolicyAsync(PrivacyPolicy privacyPolicy)
        {
            // Check if a privacy policy with the given Id exists
            var existingPolicy = await _dbContext.PrivacyPolicys
                .FirstOrDefaultAsync();

            if (existingPolicy != null)
            {
                // Update existing policy
                existingPolicy.Heading = privacyPolicy.Heading;
                existingPolicy.Content = privacyPolicy.Content;
                // Update any other properties as needed
            }
            else
            {
                // Add new policy
                await _dbContext.PrivacyPolicys.AddAsync(privacyPolicy);
            }

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return privacyPolicy;
        }
        public async Task<SessionEntity> AddSessionAsync(SessionEntity session)
        {
            // Check if a privacy policy with the given Id exists
            var existingPolicy = await _dbContext.SessionEntities
                .FirstOrDefaultAsync();

            if (existingPolicy != null)
            {
                // Update existing policy
                existingPolicy.AdminSession = session.AdminSession;
                existingPolicy.UserSession = session.UserSession;
                // Update any other properties as needed
            }
            else
            {
                // Add new policy
                await _dbContext.SessionEntities.AddAsync(session);
            }

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return session;
        }
        public async Task<PrivacyPolicy> GetPrivacyPolicyAsync()
        {
            // Check if a privacy policy with the given Id exists
            var existingPolicy = await _dbContext.PrivacyPolicys
                .FirstOrDefaultAsync();

            return existingPolicy;
        }

        public async Task<SessionEntity> GetSessionAsync()
        {
            // Check if a privacy policy with the given Id exists
            var session = await _dbContext.SessionEntities
                .FirstOrDefaultAsync();

            return session;
        }
        public async Task<LoginLog> GetLoginLogByUserIdAsync(int userId)
        {
            // Query the database for the login log entry with the highest LogId for the given userId
            return await _dbContext.LoginLogs
                .Where(log => log.UserId == userId)
                .OrderByDescending(log => log.LogId) // Order by LogId in descending order
                .FirstOrDefaultAsync(); // Get the first entry in the ordered list
        }

        public async Task<LoginLog> GetLoginLogByUserIdAsync(string email)
        {
            // Query the database for the login log entry with the highest LogId for the given userId
            return await _dbContext.LoginLogs
                .Where(log => log.Token == email)
                .OrderByDescending(log => log.LogId) // Order by LogId in descending order
                .FirstOrDefaultAsync(); // Get the first entry in the ordered list
        }

        public async Task<UserAuth> ForgotPassword(string email)
        {
            var user = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.EmailAddress == email);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<AdminAuth> ForgotPasswords(string email)
        {
            var user = await _dbContext.AdminAuths.FirstOrDefaultAsync(u => u.EmailAddress == email);

            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<ServiceResultDTO<UserAuth>> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var user = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.EmailAddress == changePasswordRequest.Email);

            ServiceResultDTO<UserAuth> serviceResultDTO = new ServiceResultDTO<UserAuth>();
            if(user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(changePasswordRequest.NewPassword, user.TPassword))
                {
                    serviceResultDTO.Success = false;
                    serviceResultDTO.Message = "Password can not be same as Transaction Password.";
                    return serviceResultDTO;
                }
                else if (BCrypt.Net.BCrypt.Verify(changePasswordRequest.CurrentPassword, user.Password))
                {
                    user.Password = changePasswordRequest.NewPassword;
                    _dbContext.UserAuths.Update(user);
                    await _dbContext.SaveChangesAsync();
                    serviceResultDTO.Data = user;
                    serviceResultDTO.Success = true;
                    serviceResultDTO.Message = "Password changed successfully.";
                    return serviceResultDTO;
                }
            }
            serviceResultDTO.Success= false;
            serviceResultDTO.Message = "Invalid credentials.";
            serviceResultDTO.Data = user;
            return serviceResultDTO;
        }
        public async Task<ServiceResultDTO<bool>> ResetPassword(Domain.Entities.ResetPasswordRequest resetPasswordRequest)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            var user = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.UserId == resetPasswordRequest.UserId);
                user.Password = resetPasswordRequest.Password;
                user.IsLocked = false;
                user.FailedLoginAttempts = 0;
            _dbContext.UserAuths.Update(user);
                var result =await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = "Password reset successfull.";
                serviceResultDTO.Data = true;
            }
             return serviceResultDTO;
        }

        public async Task<ServiceResultDTO<bool>> AdminResetPassword(Domain.Entities.ResetPasswordRequest resetPasswordRequest)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            var user = await _dbContext.AdminAuths.FirstOrDefaultAsync(u => u.UserId == resetPasswordRequest.UserId);

            user.Password = resetPasswordRequest.Password;
            user.IsLocked = false;
            user.FailedLoginAttempts = 0;
            _dbContext.AdminAuths.Update(user);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = "Password reset successfull.";
                serviceResultDTO.Data = true;
            }
            return serviceResultDTO;
        }

        public async Task<ServiceResultDTO<bool>> AdminStaffResetPassword(Domain.Entities.AdminResetPasswordRequest resetPasswordRequest)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            var user = await _dbContext.AdminAuths.FirstOrDefaultAsync(u => u.UserId == resetPasswordRequest.UserId);

            user.Password = resetPasswordRequest.Password;
            user.IsLocked = false;
            user.FailedLoginAttempts = 0;
            _dbContext.AdminAuths.Update(user);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = "Password reset successfull.";
                serviceResultDTO.Data = true;
            }
            return serviceResultDTO;
        }

        public async Task<UserAuth> GetSponsorDetails(Guid id)
        {
            var user = await (from ua in _dbContext.UserAuths
                              where ua.UserGuid == id && ua.IsDeleted == false && ua.IsActive == true
                              // Add more conditions if needed
                              select ua).FirstOrDefaultAsync();

            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserAuth> GetSponsorDetailsbySponsorCode(string sponsorCode)
        {
            var user = await (from ua in _dbContext.UserAuths
                                where  ua.IsDeleted==false && ua.IsActive==true
                              // Add more conditions if needed
                              select ua).FirstOrDefaultAsync();
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<ServiceResultDTO<bool>> CheckEmailExists(string email)
        {
            ServiceResultDTO<bool> serviceResultDTO = new ServiceResultDTO<bool>();
            var user = await _dbContext.UserAuths.FirstOrDefaultAsync(u => u.EmailAddress == email);
            if (user != null)
            {
                serviceResultDTO.Success = false;
                serviceResultDTO.Data = false;
                serviceResultDTO.Message = "Email already exists.";
            }
            else
            {
                serviceResultDTO.Success = true;
                serviceResultDTO.Data = true;
                serviceResultDTO.Message = "Email not Exists";
            }
            await _dbContext.SaveChangesAsync();
            return serviceResultDTO;
        }

  
        public async Task<ServiceResultDTO<Addresses>> AddUpdateAddresses(Addresses addresses)
        {
            ServiceResultDTO<Addresses> serviceResultDTO = new ServiceResultDTO<Addresses>();

            var existingAddress1 = await _dbContext.Addresses.CountAsync(u=>u.IsDeleted==false && u.UserId==addresses.UserId);
            if(existingAddress1==0 )
            {
                addresses.IsActive = true;
            }

            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(u => u.AddressId == addresses.AddressId);

            var addressess = await _dbContext.Addresses.Where(u => u.UserId == addresses.UserId).ToListAsync();
            if (addresses.IsActive)
            {
                foreach (var item in addressess)
                {
                    item.IsActive = false;
                }
                _dbContext.Addresses.UpdateRange(addressess);
            }

            if (existingAddress != null)
            {
                _dbContext.Entry(existingAddress).CurrentValues.SetValues(addresses);
                await _dbContext.SaveChangesAsync();
                var address = await _dbContext.Addresses
                .FirstOrDefaultAsync(u => u.AddressId == addresses.AddressId);
                serviceResultDTO.Data = address;
                serviceResultDTO.Success = true;
                serviceResultDTO.Message = ResponseConstants.AddressUpdated;
            }
            else
            {
                await _dbContext.Addresses.AddAsync(addresses);
                await _dbContext.SaveChangesAsync();
                var data = _dbContext.Addresses.Where(k => k.UserId == addresses.UserId).OrderByDescending(k => k.AddressId).FirstOrDefault();
                serviceResultDTO.Success = true;
                serviceResultDTO.Data = data;
                serviceResultDTO.Message = ResponseConstants.AddressAdded;
            }
            return serviceResultDTO;
        }

        public async Task<IEnumerable<Addresses>> GetAllAddresses(int userId)
        {
            var addresses = await _dbContext.Addresses.Where(a => a.UserId == userId && a.IsDeleted==false).OrderByDescending(a => a.IsActive).ToListAsync();
            return addresses;
        }

        public async Task<Addresses> GetAddressById(int addressId)
        {
            var addresses = await _dbContext.Addresses.FirstOrDefaultAsync(u => u.AddressId == addressId);
            return addresses;
        }

        public async Task<Addresses> GetShippingAddress(int userId)
        {
            var addresses =  _dbContext.Addresses.Where(u => u.UserId == userId && u.IsActive==true).FirstOrDefault();
            return addresses;
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var address = await _dbContext.Addresses.FindAsync(addressId);
            if (address == null)
            {
                return false;
            }
            address.IsDeleted = true; // Mark as deleted
            _dbContext.Addresses.Update(address); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetDefaltAddressAsync(int addressId)
        {
            var address = await _dbContext.Addresses.FindAsync(addressId);
            if (address == null)
            {
                return false;
            }
            var addressess = await _dbContext.Addresses.Where(u => u.UserId == address.UserId).ToListAsync();
            if (addressess.Count>0)
            {
                foreach (var item in addressess)
                {
                    item.IsActive = false;
                }
                _dbContext.Addresses.UpdateRange(addressess);
            }

            address.IsActive = true; // Mark as deleted
            _dbContext.Addresses.Update(address); // Update the entity
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<WelcomeMessage> AddWelcomeMessageAsync(WelcomeMessage welcomeMessage)
        {
            // Check if a welcome message with the given Id exists
            var existingMessage = await _dbContext.WelcomeMessages
                .FirstOrDefaultAsync(); // You may need to use a specific identifier to find the correct entity

            if (existingMessage != null)
            {
                // Update existing welcome message
                existingMessage.Heading = welcomeMessage.Heading;
                existingMessage.Content = welcomeMessage.Content;
                existingMessage.pathurl = welcomeMessage.pathurl;
                existingMessage.IsActive = welcomeMessage.IsActive;
                existingMessage.buttonname = welcomeMessage.buttonname;
                existingMessage.StartDate = welcomeMessage.StartDate;
                existingMessage.EndDate = welcomeMessage.EndDate;

                // Retain the existing image if the new one is blank or null
                existingMessage.imageUrl = string.IsNullOrEmpty(welcomeMessage.imageUrl)
                    ? existingMessage.imageUrl
                    : welcomeMessage.imageUrl;
            }
            else
            {
                // Add new welcome message
                await _dbContext.WelcomeMessages.AddAsync(welcomeMessage);
            }

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return welcomeMessage;
        }

        public async Task<WelcomeMessage> GetWelcomeMessageAsync()
        {
            // Check if a privacy policy with the given Id exists
            var existingWelcome = await _dbContext.WelcomeMessages
                .FirstOrDefaultAsync();
            return existingWelcome;
        }
    }
}
