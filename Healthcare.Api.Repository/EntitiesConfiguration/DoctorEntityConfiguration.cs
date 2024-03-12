﻿using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class DoctorEntityConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctor").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(p => p.User)
               .WithOne()
               .HasForeignKey<Doctor>(p => p.UserId)
               .IsRequired();

            builder.HasMany(d => d.HealthInsurances)
                .WithMany(hp => hp.Doctors)
                .UsingEntity<DoctorHealthInsurance>(
                    j => j
                        .HasOne(dhp => dhp.HealthInsurance)
                        .WithMany()
                        .HasForeignKey(dhp => dhp.HealthInsuranceId),
                    j => j
                        .HasOne(dhp => dhp.Doctor)
                        .WithMany()
                        .HasForeignKey(dhp => dhp.DoctorId),
                    j =>
                    {
                        j.HasKey(t => new { t.DoctorId, t.HealthInsuranceId });
                        j.ToTable("DoctorHealthInsurance");
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