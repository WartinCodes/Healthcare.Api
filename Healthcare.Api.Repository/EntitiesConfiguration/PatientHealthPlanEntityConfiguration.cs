using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class PatientHealthPlanEntityConfiguration : IEntityTypeConfiguration<PatientHealthPlan>
    {
        public void Configure(EntityTypeBuilder<PatientHealthPlan> builder)
        {
            builder.HasKey(ph => new { ph.PatientId, ph.HealthPlanId });
        }
    }
}