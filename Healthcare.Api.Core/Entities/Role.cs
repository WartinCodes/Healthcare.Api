using Microsoft.AspNetCore.Identity;

namespace Healthcare.Api.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
