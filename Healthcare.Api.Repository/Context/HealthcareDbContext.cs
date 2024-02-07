using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Healthcare.Api.Repository.Context
{
    public class HealthcareDbContext : DbContext, IHealthcareDbContext
    {
        private readonly IConfiguration _configuration;
        public const string _Schema = "healthcare";
        //public DbSet<Brand> Brands { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<SubCategory> SubCategories { get; set; }
        //public DbSet<Family> Families { get; set; }

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
