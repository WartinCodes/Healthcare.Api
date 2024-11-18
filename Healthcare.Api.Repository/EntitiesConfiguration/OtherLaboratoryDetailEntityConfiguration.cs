using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class OtherLaboratoryDetailEntityConfiguration : IEntityTypeConfiguration<OtherLaboratoryDetail>
    {
        public void Configure(EntityTypeBuilder<OtherLaboratoryDetail> builder)
        {
            builder.ToTable("OtherLaboratoryDetail").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(ol => ol.LaboratoryDetail)
                .WithMany(ld => ld.OtherLaboratoryDetails)
                .HasForeignKey(ol => ol.IdLaboratoryDetail)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
