using MLMProject.Application.DTOs;
using MLMProject.Application.IRepository;
using MLMProject.Domain.Entities;

namespace MLMProject.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResultDTO<UserAuthDTO>> Login(LoginRequestDTO user);
        Task<ServiceResultDTO<AdminAuthDTO>> AdminLogin(LoginRequestDTO user);
        Task<ServiceResultDTO<UserAuthDTO>> AddUserAsync(UserAuthDTO user);
        Task<ServiceResultDTO<UserAuthDTO>> AddUserStaffAsync(UserAuthDTO user);
        Task<ServiceResultDTO<AdminAuthDTO>> AddAdminStaffAsync(AdminAuthDTO user);
        Task<UserAuthDTO> UpdateUser(UserAuthDTO user);

        Task<AdminAuthDTO> UpdateAdmin(AdminAuthDTO user);

        Task<UserAuthDTO> UpdateUserONLogin(UserAuthDTO user);

        Task<AdminAuthDTO> UpdateAdminONLogin(AdminAuthDTO user);

        Task<UserAuthDTO> GetUserByIdAsync(int id);

        Task<AdminAuthDTO> GetAdminByIdAsync(int id);
        Task<IEnumerable<UserAuthDTO>> GetAllUsersAsync();

        Task<IEnumerable<AdminAuthDTO>> GetAllAdminAsync();

        Task<IEnumerable<UserTypeDTO>> GetAllUserTypeAsync();
        Task<string> GenerateUserCodeAsync();
        Task<string> GenerateAdminCodeAsync();
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> ActiveUserAsync(int userId);
        Task<bool> DeactiveUserAsync(int userId);

        Task<bool> DeleteAdminAsync(int userId);
        Task<bool> ActiveAdminAsync(int userId);
        Task<bool> DeactiveAdminAsync(int userId);


        Task<bool> LockUserAsync(int userId);
        Task<bool> UnlockUserAsync(int userId);

        Task<LoginLogDTO> AddLoginLogAsync(LoginLog loginLog);

        Task<PrivacyPolicyDTO> AddPrivacyPolicyAsync(PrivacyPolicy privacyPolicy);
        
        Task<SessionEntityDTO> AddSessionAsync(SessionEntity sessionEntity);
        Task<PrivacyPolicyDTO> GetPrivacyPolicyAsync();
      
        Task<SessionEntityDTO> GetSessionAsync();
        Task<UserAuthDTO> GetSponsorDetails(Guid id);
        Task<UserAuthDTO> GetSponsorDetailsbySponsorCode(string sponsorCode);
        Task<LoginLogDTO> GetLoginLogByUserIdAsync(int userId);
        Task<LoginLogDTO> GetLoginLogByUserIdAsync(string email);

        Task<ServiceResultDTO<UserAuthDTO>> ForgotPassword(string user);
        Task<ServiceResultDTO<AdminAuthDTO>> ForgotPasswords(string user);
        Task<ServiceResultDTO<ChangePasswordRequestDTO>> ChangePasswordAsync(ChangePasswordRequestDTO changePassword);

        Task<ServiceResultDTO<bool>> ResetPasswordAsync(ResetPasswordRequestDTO changePassword);
        Task<ServiceResultDTO<bool>> AdminResetPasswordAsync(ResetPasswordRequestDTO changePassword);
        Task<ServiceResultDTO<bool>> AdminStaffResetPasswordAsync(AdminResetPasswordRequestDTO changePassword);
        Task<ServiceResultDTO<bool>> CheckEmailExists(string email);

        #region Users
        Task<ServiceResultDTO<AddressesDTO>> AddUpdateAddresses(AddressesDTO addressesDTO);
        Task<IEnumerable<AddressesDTO>> GetAllAddresses(int userId);
        Task<AddressesDTO> GetAddressById(int id);
        Task<AddressesDTO> GetShippingAddressAsync(int userId);

        Task<bool> DeleteAddressAsync(int addressId);
        Task<bool> SetDefaltAddressAsync(int addressId);
        Task<WelcomeMessageDTO> AddWelcomeMessageAsync(WelcomeMessage welcomeMessage);
        Task<WelcomeMessageDTO> GetWelcomeMessageAsync();

        #endregion Users
    }
}
