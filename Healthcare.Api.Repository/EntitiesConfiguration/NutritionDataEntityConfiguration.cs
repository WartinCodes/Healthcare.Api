using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class NutritionDataEntityConfiguration : IEntityTypeConfiguration<NutritionData>
    {
        public void Configure(EntityTypeBuilder<NutritionData> builder)
        {
            builder.ToTable("NutritionData").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Weight).IsRequired(false);
            builder.Property(x => x.Difference).IsRequired(false);
            builder.Property(x => x.FatPercentage).IsRequired(false);
            builder.Property(x => x.MusclePercentage).IsRequired(false);
            builder.Property(x => x.VisceralFat).IsRequired(false);
            builder.Property(x => x.IMC).IsRequired(false);
            builder.Property(x => x.TargetWeight).IsRequired(false);
            builder.Property(x => x.Observations).HasMaxLength(500).IsRequired(false);

            builder.HasOne(nd => nd.Patient)
                .WithMany(p => p.NutritionData)
                .HasForeignKey(nd => nd.PatientId) 
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
