using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Healthcare.Api.Repository.Context
{
    public class HealthcareDbContext : DbContext, IHealthcareDbContext
    {
        private readonly IConfiguration _configuration;
        public const string _Schema = "healthcare";

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public HealthcareDbContext(IConfiguration configuration) : base()
        {
            this._configuration = configuration;
        }

        public HealthcareDbContext(IConfiguration configuration, DbContextOptions<HealthcareDbContext> options) : base(options)
        {
            this._configuration = configuration;
        }

        public HealthcareDbContext(DbContextOptions<HealthcareDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(_Schema);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
