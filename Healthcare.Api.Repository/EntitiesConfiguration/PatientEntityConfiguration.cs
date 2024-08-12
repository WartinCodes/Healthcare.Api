using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class PatientEntityConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient").HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.AffiliationNumber).IsRequired(false);
            builder.Property(p => p.Observations).IsRequired(false);
            builder.Property(p => p.Died).IsRequired(false);

            builder.HasOne(p => p.User)
               .WithOne()
               .HasForeignKey<Patient>(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.HealthPlans)
                .WithMany(hp => hp.Patients)
                .UsingEntity<PatientHealthPlan>(
                    j => j
                        .HasOne(ph => ph.HealthPlan)
                        .WithMany()
                        .HasForeignKey(ph => ph.HealthPlanId),
                    j => j
                        .HasOne(ph => ph.Patient)
                        .WithMany()
                        .HasForeignKey(ph => ph.PatientId),
                    j =>
                    {
                        j.HasKey(t => new { t.PatientId, t.HealthPlanId });
                        j.ToTable("PatientHealthPlan");
                    }
                );
        }
    }
}