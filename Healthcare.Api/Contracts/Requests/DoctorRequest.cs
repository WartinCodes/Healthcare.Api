namespace Healthcare.Api.Contracts.Requests
{
    public class DoctorRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public string Matricula { get; set; }
        public AddressRequest Address { get; set; }
        public List<SpecialityRequest> Specialities { get; set; }
        public List<HealthInsuranceRequest> HealthInsurances { get; set; }
        public int? RegisteredById { get; set; }
        public string? CUIL { get; set; }
        public string? CUIT { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? BloodType { get; set; }
        public string? RhFactor { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
    }
}
