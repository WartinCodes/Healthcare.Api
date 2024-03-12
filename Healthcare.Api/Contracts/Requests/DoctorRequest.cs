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
        public List<HealthInsuranceRequest> HealthInsurance { get; set; }
    }
}
