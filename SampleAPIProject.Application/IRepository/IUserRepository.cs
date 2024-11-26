using MLMProject.Application.DTOs;
using MLMProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLMProject.Application.IRepository
{
    public interface IUserRepository
    {
        Task<UserAuth> Login(LoginRequest user);
        Task<AdminAuth> AdminLogin(LoginRequest user);
        Task<ServiceResultDTO<UserAuth>> AddUserAsync(UserAuth user);
        Task<ServiceResultDTO<UserAuth>> AddUserStaffAsync(UserAuth user);
        Task<ServiceResultDTO<AdminAuth>> AddAdminStaffAsync(AdminAuth user);
        Task<UserAuth> UpdateUserAsync(UserAuth user);
        Task<AdminAuth> UpdateAdminAsync(AdminAuth user);

        Task<UserAuth> UpdateUserONLogin(UserAuth user);

        Task<AdminAuth> UpdateAdminONLogin(AdminAuth user);

        Task<UserAuth> GetUserByIdAsync(int id);
        Task<AdminAuth> GetAdminByIdAsync(int id);
        Task<IEnumerable<UserAuth>> GetAllUsersAsync();

        Task<IEnumerable<AdminAuth>> GetAllAdminAsync();
        Task<IEnumerable<UserTypeEntity>> GetAllUserTypeAsync();
        Task<bool> DeleteUserAsync(int userId);  // Add this line
        Task<bool> ActiveUserAsync(int userId);  // Add this line
        Task<bool> DeactiveUserAsync(int userId);  // Add this line


        Task<bool> DeleteAdminAsync(int userId);  // Add this line
        Task<bool> ActiveAdminAsync(int userId);  // Add this line
        Task<bool> DeactiveAdminAsync(int userId);  // Add this line


        Task<bool> LockUserAsync(int userId);  // Add this line
        Task<bool> UnlockUserAsync(int userId);  // Add this line
        Task<int> GetUserCountAsync();
        Task<int> GetAdminCountAsync();
        Task<LoginLog> AddLoginLogAsync(LoginLog loginLog);
        Task<PrivacyPolicy> AddPrivacyPolicyAsync(PrivacyPolicy privacyPolicy);
        Task<SessionEntity> AddSessionAsync(SessionEntity session);
        Task<PrivacyPolicy> GetPrivacyPolicyAsync();
        Task<SessionEntity> GetSessionAsync();
        Task<LoginLog> GetLoginLogByUserIdAsync(int userId);
        Task<LoginLog> GetLoginLogByUserIdAsync(string email);
        Task<UserAuth> ForgotPassword(string email);
        Task<AdminAuth> ForgotPasswords(string email);
        Task<ServiceResultDTO<UserAuth>> ChangePassword(ChangePasswordRequest user);
        Task<ServiceResultDTO<bool>> ResetPassword(ResetPasswordRequest user);
        Task<ServiceResultDTO<bool>> AdminResetPassword(ResetPasswordRequest user);
        Task<ServiceResultDTO<bool>> AdminStaffResetPassword(AdminResetPasswordRequest user);
        Task<UserAuth> GetSponsorDetails(Guid Id);
        Task<ServiceResultDTO<bool>> CheckEmailExists(string email);
        Task<UserAuth> GetSponsorDetailsbySponsorCode(string sponsorCode);
        //Task<Counts> GetCounts();

        #region Users
        Task<ServiceResultDTO<Addresses>> AddUpdateAddresses(Addresses addresses);
        Task<IEnumerable<Addresses>> GetAllAddresses(int userId);
        Task<Addresses> GetAddressById(int addressId);
        Task<Addresses> GetShippingAddress(int userId);

        Task<bool> DeleteAddressAsync(int addressId);
        Task<bool> SetDefaltAddressAsync(int addressId);
        Task<WelcomeMessage> AddWelcomeMessageAsync(WelcomeMessage welcomeMessage);
        Task<WelcomeMessage> GetWelcomeMessageAsync();
        #endregion Users
    }
}
