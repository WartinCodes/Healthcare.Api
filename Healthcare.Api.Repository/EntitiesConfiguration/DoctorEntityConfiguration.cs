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
               .HasForeignKey<Doctor>(p => p.IdUser)
               .IsRequired();

            builder.HasMany(d => d.Specialities)
                   .WithOne()
                   .HasForeignKey(s => s.IdRole)
                   .IsRequired();
        }
    }
}