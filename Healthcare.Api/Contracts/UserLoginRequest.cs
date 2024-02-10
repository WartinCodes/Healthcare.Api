namespace Healthcare.Api.Contracts
{
    public class UserLoginRequest
    {
        public string NationalIdentityDocument { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
