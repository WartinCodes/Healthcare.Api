using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(u => u.UserName).IsRequired(false);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.NationalIdentityDocument).IsRequired();
            builder.Property(u => u.Email).IsRequired(false);
            builder.Property(u => u.EmailConfirmed).IsRequired();
            builder.Property(u => u.BirthDate).IsRequired();
            builder.Property(u => u.Photo).IsRequired(false);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired(false);
            builder.Property(u => u.LockoutEnabled).IsRequired();
            builder.Property(u => u.AccessFailedCount).IsRequired();
            builder.Property(u => u.LastActivityDate).IsRequired(false);
            builder.Property(u => u.LastLoginDate).IsRequired(false);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.IdUser);
        }
    }
}