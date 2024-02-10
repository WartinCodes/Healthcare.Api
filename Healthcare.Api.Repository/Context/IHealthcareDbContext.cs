using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Context
{
    public interface IHealthcareDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
    }
}
