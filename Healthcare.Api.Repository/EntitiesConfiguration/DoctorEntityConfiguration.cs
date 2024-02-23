using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class DoctorEntityConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctor").HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.HasOne(p => p.User)
               .WithOne()
               .HasForeignKey<Doctor>(p => p.UserId)
               .IsRequired();

            builder.HasMany(d => d.HealthPlans)
                .WithMany(hp => hp.Doctors)
                .UsingEntity<DoctorHealthPlan>(
                    j => j
                        .HasOne(dhp => dhp.HealthPlan)
                        .WithMany()
                        .HasForeignKey(dhp => dhp.HealthPlanId),
                    j => j
                        .HasOne(dhp => dhp.Doctor)
                        .WithMany()
                        .HasForeignKey(dhp => dhp.DoctorId),
                    j =>
                    {
                        j.HasKey(t => new { t.DoctorId, t.HealthPlanId });
                        j.ToTable("DoctorHealthPlan");
                    }
                );

            builder.HasMany(d => d.DoctorSpecialities)
                .WithOne(ds => ds.Doctor)
                .HasForeignKey(ds => ds.DoctorId);

            builder.HasOne(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.IdAddress);
        }
    }
}