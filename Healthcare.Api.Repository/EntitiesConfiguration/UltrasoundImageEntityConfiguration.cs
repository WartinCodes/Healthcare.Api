using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class UltrasoundImageEntityConfiguration : IEntityTypeConfiguration<UltrasoundImage>
    {
        public void Configure(EntityTypeBuilder<UltrasoundImage> builder)
        {
            builder.ToTable("UltrasoundImage").HasKey(x => x.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.LocationS3).IsRequired();

            builder.HasOne(x => x.Study)
               .WithMany()
               .HasForeignKey(x => x.IdStudy)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);
        }
    }
}