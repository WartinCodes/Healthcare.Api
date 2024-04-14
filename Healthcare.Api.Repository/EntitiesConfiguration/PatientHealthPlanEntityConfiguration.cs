using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class PatientHealthPlanEntityConfiguration : IEntityTypeConfiguration<PatientHealthPlan>
    {
        public void Configure(EntityTypeBuilder<PatientHealthPlan> builder)
        {
            builder.HasKey(ph => ph.Id);
            builder.Property(ph => ph.Id).ValueGeneratedOnAdd();

            builder.Property(ph => ph.PatientId);
            builder.Property(ph => ph.HealthPlanId);

            builder.HasIndex(ph => new { ph.PatientId, ph.HealthPlanId }).IsUnique();
        }
    }
}