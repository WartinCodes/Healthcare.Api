using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class DoctorSpecialityEntityConfiguration : IEntityTypeConfiguration<DoctorSpeciality>
    {
        public void Configure(EntityTypeBuilder<DoctorSpeciality> builder)
        {
            builder.HasKey(ds => new { ds.DoctorId, ds.SpecialityId });
        }
    }
}