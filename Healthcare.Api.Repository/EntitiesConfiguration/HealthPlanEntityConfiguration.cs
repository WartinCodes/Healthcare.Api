using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class HealthPlanEntityConfiguration : IEntityTypeConfiguration<HealthPlan>
    {
        public void Configure(EntityTypeBuilder<HealthPlan> builder)
        {
            builder.ToTable("HealthPlan").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.HealthInsurance)
                .WithMany(x => x.HealthPlans)
                .HasForeignKey(x => x.HealthInsuranceId)
                .IsRequired();
        }
    }
}