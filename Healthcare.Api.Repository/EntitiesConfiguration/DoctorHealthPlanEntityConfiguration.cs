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

            builder.HasKey(dhp => new { dhp.DoctorId, dhp.HealthPlanId });
        }
    }
}
