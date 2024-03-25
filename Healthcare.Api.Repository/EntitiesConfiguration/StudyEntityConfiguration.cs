using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class StudyEntityConfiguration : IEntityTypeConfiguration<Study>
    {
        public void Configure(EntityTypeBuilder<Study> builder)
        {
            builder.ToTable("Study").HasKey(x => x.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.LocationS3).IsRequired();
            builder.Property(s=> s.Note).IsRequired(false);

            builder.HasOne(s => s.StudyType)
               .WithOne()
               .HasForeignKey<Study>(s => s.StudyTypeId)
               .IsRequired();

            builder.HasOne(e => e.StudyType)
               .WithMany()
               .HasForeignKey(e => e.StudyTypeId);

            builder.HasOne(s => s.Patient)
                .WithMany(s => s.Studies)
                .HasForeignKey(s => s.PatientId)
                .IsRequired();
        }
    }
}