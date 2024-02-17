using Microsoft.AspNetCore.Identity;

namespace Healthcare.Api.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<Speciality> Specialities { get; set; }
    }
}
