using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
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
            return $"{baseUrl}/nueva-contraseña?token={token}";
        }

        public async Task SendForgotPasswordEmailAsync(string email, string fullName, string resetLink)
        {
            try
            {
                var messageBody = $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: auto;
                                background-color: #ffffff;
                                padding: 20px;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }}
                            h1 {{
                                color: #333333;
                            }}
                            p {{
                                color: #666666;
                            }}
                            a {{
                                color: #007bff;
                                text-decoration: none;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Recuperación de Contraseña</h1>
                            <p>Estimado {fullName},</p>
                            <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta. Si no has solicitado este cambio, puedes ignorar este mensaje.</p>
                            <p>Para restablecer tu contraseña, haz clic en el siguiente enlace:</p>
                            <p><a href='{resetLink}'>{resetLink}</a></p>
                            <p>Este enlace expirará en 24 horas. Si necesitas ayuda adicional, por favor contáctanos.</p>
                            <p>¡Gracias por confiar en nosotros!</p>
                            <p>Atentamente,</p>
                            <p>Incor - Centro Médico</p>
                        </div>
                    </body>
                    </html>
                ";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                    Subject = "Restablecer contraseña",
                    Body = messageBody,
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
                var messageBody = $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: auto;
                                background-color: #ffffff;
                                padding: 20px;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }}
                            h1 {{
                                color: #333333;
                            }}
                            p {{
                                color: #666666;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Solicitud de soporte técnico</h1>
                            <p><strong>Usuario:</strong> {userName}</p>
                            <p><strong>Estado:</strong> {support.Status}</p>
                            <p><strong>Prioridad:</strong> {support.Priority}</p>
                            <p><strong>Módulo:</strong> {support.Module}</p>
                            <p><strong>Descripción:</strong> {support.Description}</p>
                            <p><strong>Fecha de reporte:</strong> {support.ReportDate}</p>
                            <p><strong>Fecha de resolución:</strong> {support.ResolutionDate}</p>
                        </div>
                    </body>
                    </html>
                ";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                    Subject = $"Soporte técnico - {support.ReportDate} - {support.Status}",
                    Body = messageBody,
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

        public async Task SendEmailForNewStudyAsync(string email, string fullName)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var messageBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 20px;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 5px;
                            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                        }}
                        h1 {{
                            color: #333333;
                        }}
                        p {{
                            color: #666666;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>Nuevo estudio disponible en tu cuenta</h1>
                        <p>Hola {fullName},</p>
                        <p>Nos complace informarte que se ha subido un nuevo estudio a tu cuenta. Este estudio está ahora disponible para que lo revises y accedas a su contenido.</p>
                        <p>Para ver el estudio, inicia sesión en tu cuenta y navega hasta la sección correspondiente. Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en contactarnos.</p>
                        <p>¡Gracias por confiar en nosotros!</p>
                        <p>Atentamente,</p>
                        <p>Incor - Centro Médico</p>
                    </div>
                </body>
                </html>
            ";

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                        Subject = "Incor Centro Médico - Aviso de Estudio Disponible",
                        Body = messageBody,
                        IsBodyHtml = true,
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