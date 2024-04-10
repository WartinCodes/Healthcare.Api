using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Healthcare.Api.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IConfiguration _configuration;

        public EmailService(IOptions<SmtpSettings> smtpSettings, IConfiguration configuration)
        {
            _smtpSettings = smtpSettings?.Value ?? throw new ArgumentNullException(nameof(SmtpSettings));
            _configuration = configuration;
        }

        public string GenerateResetPasswordLink(string email, string token)
        {
            var baseUrl = _configuration.GetValue<string>("BaseUrl");

            return $"{baseUrl}/reset-password?token={Uri.EscapeDataString(token)}";
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                using (var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
                {
                    client.EnableSsl = _smtpSettings.UseSsl;
                    client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                    await client.SendMailAsync(mailMessage, cancellationToken: default);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el mail: {ex}");
                throw;
            }
        }
    }
}