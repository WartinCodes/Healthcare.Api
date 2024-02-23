using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class StateEntityConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("State").HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(s => s.Country)
                .WithMany(c => c.States)
                .HasForeignKey(s => s.IdCountry)
                .IsRequired();
        }
    }
}
