using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address").HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(a => a.Street)
                .IsRequired()
                .HasColumnName("Street")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnName("Number")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(a => a.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsUnicode(true);

            builder.Property(a => a.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(a => a.City)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CityId)
                .IsRequired();

        }
    }
}
