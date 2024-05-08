using Microsoft.AspNetCore.Identity;

namespace Healthcare.Api.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? ResetPasswordToken { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? RegisteredById { get; set; }
    }
}