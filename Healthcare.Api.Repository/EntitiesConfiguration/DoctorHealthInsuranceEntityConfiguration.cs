using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class DoctorHealthInsuranceEntityConfiguration : IEntityTypeConfiguration<DoctorHealthInsurance>
    {
        public void Configure(EntityTypeBuilder<DoctorHealthInsurance> builder)
        {
            builder.HasKey(dhp => dhp.Id);
            builder.Property(dhp => dhp.Id).ValueGeneratedOnAdd();

            builder.Property(dhp => dhp.DoctorId);
            builder.Property(dhp => dhp.HealthInsuranceId);

            builder.HasIndex(dhp => new { dhp.DoctorId, dhp.HealthInsuranceId }).IsUnique();
        }
    }
}
