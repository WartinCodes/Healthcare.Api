namespace Healthcare.Api.Contracts.Requests
{
    public class ChangePasswordRequest
    {
        public string UserId { get; set; }
        public string ActualPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
