using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Healthcare.Api.Service.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using (var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                client.EnableSsl = _smtpSettings.UseSsl;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}