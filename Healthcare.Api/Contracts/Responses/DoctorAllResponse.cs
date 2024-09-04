namespace Healthcare.Api.Contracts.Responses
{
    public class DoctorAllResponse
    {
   


        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Matricula { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public ICollection<SpecialityResponse> Specialities { get; set; }
        public string PhoneNumber { get; set; }
    }
}