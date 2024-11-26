using AutoMapper;
using MLMProject.Application.DTOs;
using MLMProject.Domain.Entities;


namespace MLMProject.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map between LoginRequestDTO and LoginRequest entity
            CreateMap<LoginRequestDTO, LoginRequest>().ReverseMap();

            // Map between UserAuthDTO and UserAuth entity
            CreateMap<UserAuth, UserAuthDTO>().ReverseMap();

            // Map between LoginLogDTO and LoginLog entity
            CreateMap<LoginLog, LoginLogDTO>().ReverseMap();

            // Map between UserTypeDTO and UserTypeEntity entity
            CreateMap<UserTypeEntity, UserTypeDTO>().ReverseMap();

            // Map between PrivacyPolicyDTO and PrivacyPolicy entity
            CreateMap<PrivacyPolicy, PrivacyPolicyDTO>().ReverseMap();

            // Map between ChangePasswordRequestDTO and ChangePasswordRequest entity
            CreateMap<ChangePasswordRequest, ChangePasswordRequestDTO>().ReverseMap();

            // Map between ResetPasswordRequestDTO and ResetPasswordRequest entity
            CreateMap<ResetPasswordRequest, ResetPasswordRequestDTO>().ReverseMap();

            // Map between AdminResetPasswordRequestDTO and AdminResetPasswordRequest entity
            CreateMap<AdminResetPasswordRequest, AdminResetPasswordRequestDTO>().ReverseMap();

            // Map between KYCEntitiesDTO and KYCEntities entity
            CreateMap<KYCEntities, KYCEntitiesDTO>().ReverseMap();

            // Map between ErrorEntitiesDTO and ErrorEntities entity
            CreateMap<ErrorEntities, ErrorEntitiesDTO>().ReverseMap();

            // Map between AdminAuthDTO and AdminAuth entity
            CreateMap<AdminAuth, AdminAuthDTO>().ReverseMap();

            // Map between AddressesDTO and Addresses entity
            CreateMap<Addresses, AddressesDTO>().ReverseMap();

            // Map between SessionEntityDTO and SessionEntity entity
            CreateMap<SessionEntity, SessionEntityDTO>().ReverseMap();

            // Map between WelcomeMessageDTO and WelcomeMessage entity
            CreateMap<WelcomeMessage, WelcomeMessageDTO>().ReverseMap();

        }
    }
}
