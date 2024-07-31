namespace Healthcare.Api.Contracts.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressResponse Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? BloodType { get; set; }
        public string? RhFactor { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public string? CUIL { get; set; }
        public string? CUIT { get; set; }
    }
}
