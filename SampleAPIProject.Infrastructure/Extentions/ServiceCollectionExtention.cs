using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MLMProject.Infrastructure.Persistance;
using MLMProject.Infrastructure.Repository;
using MLMProject.Infrastructure.Seeders;


namespace MLMProject.Infrastructure.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            Console.WriteLine(connectionString);
            services.AddDbContext<MLMProjectDbContext>(options=> options.UseSqlServer(connectionString));
            //services.AddIdentityCore<UserAuth>();
            services.AddScoped<IUserSeeders , UserSeeders>();

        }
    }
}
