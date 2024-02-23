using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class HealthInsuranceEntityConfiguration : IEntityTypeConfiguration<HealthInsurance>
    {
        public void Configure(EntityTypeBuilder<HealthInsurance> builder)
        {
            builder.ToTable("HealthInsurance").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired(); 

            builder.HasMany(x => x.HealthPlans)
                .WithOne(x => x.HealthInsurance)
                .HasForeignKey(x => x.HealthInsuranceId)
                .IsRequired();
        }
    }
}
