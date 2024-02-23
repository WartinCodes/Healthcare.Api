using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class DoctorHealthPlanEntityConfiguration : IEntityTypeConfiguration<DoctorHealthPlan>
    {
        public void Configure(EntityTypeBuilder<DoctorHealthPlan> builder)
        {
            builder.HasKey(dhp => dhp.Id);
            builder.Property(dhp => dhp.Id).ValueGeneratedOnAdd();

            builder.Property(dhp => dhp.DoctorId);
            builder.Property(dhp => dhp.HealthPlanId);

            builder.HasIndex(dhp => new { dhp.DoctorId, dhp.HealthPlanId }).IsUnique();
        }
    }
}
