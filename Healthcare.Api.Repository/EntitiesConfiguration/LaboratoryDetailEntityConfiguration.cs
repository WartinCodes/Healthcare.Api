using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class LaboratoryDetailEntityConfiguration : IEntityTypeConfiguration<LaboratoryDetail>
    {
        public void Configure(EntityTypeBuilder<LaboratoryDetail> builder)
        {
            builder.ToTable("LaboratoryDetail").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Study)
               .WithMany()
               .HasForeignKey(x => x.IdStudy)
               .IsRequired(false);
        }
    }
}
