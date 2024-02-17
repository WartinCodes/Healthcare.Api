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
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(x => x.CUIL);

            builder.HasOne(p => p.User)
               .WithOne()
               .HasForeignKey<Patient>(p => p.IdUser)
               .IsRequired();
        }
    }
}