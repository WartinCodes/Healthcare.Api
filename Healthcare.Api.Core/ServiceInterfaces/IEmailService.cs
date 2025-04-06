namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IEmailService
    {
        string GenerateResetPasswordLink(string email, string token);
        Task SendForgotPasswordEmailAsync(string email, string fullName, string resetLink);
        Task SendEmailForNewStudyAsync(string email, string fullName, DateTime studyDate);
        Task SendWelcomeEmailAsync(string email, string userName, string fullName);
    }
}