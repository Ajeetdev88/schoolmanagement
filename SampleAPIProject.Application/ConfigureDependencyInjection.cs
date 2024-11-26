using Microsoft.Extensions.DependencyInjection;
using MLMProject.Application.Interfaces;
using MLMProject.Application.IRepository;
using MLMProject.Application.IService;
using MLMProject.Application.Services;

namespace MLMProject.Application
{
    public static class ConfigureDependencyInjection
    {
        public static IServiceCollection ImplementServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
