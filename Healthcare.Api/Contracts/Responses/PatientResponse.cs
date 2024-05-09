namespace Healthcare.Api.Contracts.Responses
{
    public class PatientResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public AddressResponse Address { get; set; }
        public ICollection<HealthPlanResponse> HealthPlans { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? RegisteredById { get; set; }
    }
}