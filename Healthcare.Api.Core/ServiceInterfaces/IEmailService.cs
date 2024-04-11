using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IEmailService
    {
        string GenerateResetPasswordLink(string email, string token);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task SendEmailForNewStudyAsync(string email, string fullName);
        Task SendEmailSupportAsync(string userName, Support support);
    }
}
