using Microsoft.EntityFrameworkCore;
using MLMProject.Domain.Entities;


namespace MLMProject.Infrastructure.Persistance
{
    public class MLMProjectDbContext : DbContext
    {
        public MLMProjectDbContext(DbContextOptions<MLMProjectDbContext> options) : base(options)
        {

        }

        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<AdminAuth> AdminAuths { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<UserTypeEntity> UserTypes { get; set; }
        public DbSet<CustomerTypeEntities> CustomerTypes { get; set; }
        public DbSet<PrivacyPolicy> PrivacyPolicys { get; set; }
        public DbSet<ErrorEntities> ErrorEntitiess { get; set; }
        public DbSet<PositionEntities> PositionEntities { get; set; }
        public DbSet<WelcomeMessage> WelcomeMessages { get; set; }
        public DbSet<Addresses> Addresses { get; set; }

        //session
        public DbSet<SessionEntity> SessionEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
