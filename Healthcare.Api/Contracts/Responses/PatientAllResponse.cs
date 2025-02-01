namespace Healthcare.Api.Contracts.Responses
{
    public class PatientAllResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public ICollection<HealthPlanResponse> HealthPlans { get; set; }
        public string PhoneNumber { get; set; }
    }
}