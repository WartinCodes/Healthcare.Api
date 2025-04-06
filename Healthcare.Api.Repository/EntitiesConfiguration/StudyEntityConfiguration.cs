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
            builder.Property(s => s.Note).IsRequired(false);
            builder.Property(s => s.Created).IsRequired(false);

            builder.HasOne(s => s.StudyType)
                .WithMany(st => st.Studies)
                .HasForeignKey(s => s.StudyTypeId);

            builder.HasOne(s => s.User)
                .WithMany(s => s.Studies)
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            builder.HasOne(s => s.SignedDoctor)
                .WithMany(d => d.SignedStudies)
                .HasForeignKey(s => s.SignedDoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
