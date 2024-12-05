using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class BloodTestDataEntityConfiguration : IEntityTypeConfiguration<BloodTestData>
    {
        public void Configure(EntityTypeBuilder<BloodTestData> builder)
        {
            builder.ToTable("BloodTestData").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Value)
                .HasColumnName("Value")
                .IsRequired()
                .IsUnicode(false);

            builder.HasOne(x => x.BloodTest)
                .WithMany()
                .HasForeignKey(x => x.IdBloodTest)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
            builder.HasOne(x => x.Study)
                .WithMany()
                .HasForeignKey(x => x.IdStudy)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}