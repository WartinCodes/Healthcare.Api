namespace Healthcare.Api.Contracts
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentityDocument { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        //public string Photo { get; set; }
    }
}