using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class BloodTestEntityConfiguration : IEntityTypeConfiguration<BloodTest>
    {
        public void Configure(EntityTypeBuilder<BloodTest> builder)
        {
            builder.ToTable("BloodTest").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.ReferenceValue)
                .HasColumnName("ReferenceValue")
                .IsRequired(false)
                .IsUnicode(false);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.IdUnit) 
                .OnDelete(DeleteBehavior.Restrict) 
                .IsRequired();
        }
    }
}