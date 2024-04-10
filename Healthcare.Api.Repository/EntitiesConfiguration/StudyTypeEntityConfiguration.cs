using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class StudyTypeEntityConfiguration : IEntityTypeConfiguration<StudyType>
    {
        public void Configure(EntityTypeBuilder<StudyType> builder)
        {
            builder.HasKey(st => st.Id);
            builder.Property(st => st.Id).ValueGeneratedOnAdd();

            builder.Property(st => st.Name).IsRequired();
        }
    }
}