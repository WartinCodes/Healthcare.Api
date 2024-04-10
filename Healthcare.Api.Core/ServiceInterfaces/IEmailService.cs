namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IEmailService
    {
        string GenerateResetPasswordLink(string email, string token);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
