using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Healthcare.Api.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly TemplateConfiguration _templateConfiguration;
        private readonly IConfiguration _configuration;
        private readonly IFileHelper _fileHelper;

        public EmailService(
            IOptions<SmtpSettings> smtpSettings,
            IOptions<TemplateConfiguration> templateConfiguration,
            IFileHelper fileHelper,
            IConfiguration configuration)
        {
            _smtpSettings = smtpSettings?.Value ?? throw new ArgumentNullException(nameof(SmtpSettings));
            _templateConfiguration = templateConfiguration?.Value ?? throw new ArgumentNullException(nameof(TemplateConfiguration));
            _configuration = configuration;
            _fileHelper = fileHelper;
        }

        public string GenerateResetPasswordLink(string email, string token)
        {
            var baseUrl = _configuration.GetValue<string>("BaseUrl");
            return $"{baseUrl}/nueva-contraseña?token={token}";
        }

        public async Task SendForgotPasswordEmailAsync(string email, string fullName, string resetLink)
        {
            try
            {
                var forgotPasswordTemplate = Path.Combine(_fileHelper.GetExecutingDirectory(), _templateConfiguration.ForgotPasswordTemplate);
                var forgotPasswordTemplateHtml = new StringBuilder(_fileHelper.ReadAllText(forgotPasswordTemplate));
                forgotPasswordTemplateHtml = forgotPasswordTemplateHtml.Replace("<%FullName%>", fullName);
                forgotPasswordTemplateHtml = forgotPasswordTemplateHtml.Replace("<%ResetLink%>", resetLink);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                    Subject = "Restablecer contraseña",
                    Body = forgotPasswordTemplateHtml.ToString(),
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

        public async Task SendEmailSupportAsync(string userName, Support support)
        { 
            try
            {
                var supportTemplate = Path.Combine(_fileHelper.GetExecutingDirectory(), _templateConfiguration.SupportTemplate);
                var supportTemplateHtml = new StringBuilder(_fileHelper.ReadAllText(supportTemplate));
                supportTemplateHtml = supportTemplateHtml.Replace("<%UserName%>", userName);
                supportTemplateHtml = supportTemplateHtml.Replace("<%Status%>", support.Status);
                supportTemplateHtml = supportTemplateHtml.Replace("<%Priority%>", support.Priority);
                supportTemplateHtml = supportTemplateHtml.Replace("<%Module%>", support.Module);
                supportTemplateHtml = supportTemplateHtml.Replace("<%Description%>", support.Description);
                supportTemplateHtml = supportTemplateHtml.Replace("<%ReportDate%>", support.ReportDate.ToString("dd/MM/yyyy"));
                supportTemplateHtml = supportTemplateHtml.Replace("<%ResolutionDate%>", support.ResolutionDate?.ToString("dd/MM/yyyy") ?? "N/A");

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                    Subject = $"Soporte técnico - {support.ReportDate} - {support.Status}",
                    Body = supportTemplateHtml.ToString(),
                    IsBodyHtml = true
                };
                mailMessage.To.Add("soporte@rosariodata.com");

                using (var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
                {
                    client.EnableSsl = _smtpSettings.UseSsl;
                    client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el mail: {ex}");
                throw;
            }
        }

        public async Task SendEmailForNewStudyAsync(string email, string fullName, DateTime studyDate)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var newStudyTemplate = Path.Combine(_fileHelper.GetExecutingDirectory(), _templateConfiguration.NewStudyTemplate);
                    var newStudyTemplateHtml = new StringBuilder(_fileHelper.ReadAllText(newStudyTemplate));
                    newStudyTemplateHtml = newStudyTemplateHtml.Replace("<%FullName%>", fullName);
                    newStudyTemplateHtml = newStudyTemplateHtml.Replace("<%StudyDate%>", studyDate.ToString("dd/MM/yyyy"));

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                        Subject = "Incor Centro Médico - Aviso de Estudio Disponible",
                        Body = newStudyTemplateHtml.ToString(),
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
                else
                {
                    await Task.CompletedTask;
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