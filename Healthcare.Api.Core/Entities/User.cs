using Microsoft.AspNetCore.Identity;

namespace Healthcare.Api.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public override string? Email { get; set; }
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
        public string? CUIL { get; set; }
        public string? CUIT { get; set; }
        public int? RegisteredById { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? BloodType { get; set; }
        public string? RhFactor { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
        public virtual ICollection<NutritionData> NutritionData { get; set; }
    }
}