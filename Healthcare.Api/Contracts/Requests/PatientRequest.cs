namespace Healthcare.Api.Contracts.Requests
{
    public class PatientRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public AddressRequest Address { get; set; }
        public List<HealthPlanRequest> HealthPlans { get; set; }
        public int? RegisteredById { get; set; }
        public string? CUIL { get; set; }
        public string? CUIT { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? BloodType { get; set; }
        public string? RhFactor { get; set; }
        public string? MaritalStatus { get; set; }
        public string? AffiliationNumber { get; set; }
        public string? Gender { get; set; }
        public string? Observations { get; set; }
        public string? Died { get; set; }
    }
}