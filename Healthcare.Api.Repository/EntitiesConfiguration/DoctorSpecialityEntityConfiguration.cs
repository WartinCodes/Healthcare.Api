using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class DoctorSpecialityEntityConfiguration : IEntityTypeConfiguration<DoctorSpeciality>
    {
        public void Configure(EntityTypeBuilder<DoctorSpeciality> builder)
        {
            builder.HasKey(ds => ds.Id);
            builder.Property(ds => ds.Id).ValueGeneratedOnAdd();

            builder.Property(ds => ds.DoctorId);
            builder.Property(ds => ds.SpecialityId);

            builder.HasIndex(ds => new { ds.DoctorId, ds.SpecialityId }).IsUnique();
        }
    }
}