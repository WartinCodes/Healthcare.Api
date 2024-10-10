namespace Healthcare.Api.Contracts.Requests
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string Photo { get; set; }
        public AddressRequest Address { get; set; }
    }
}
