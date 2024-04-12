using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Contracts.Requests
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public AddressRequest Address { get; set; }
    }
}
