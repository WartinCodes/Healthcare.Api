﻿using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IEmailService
    {
        string GenerateResetPasswordLink(string email, string token);
        Task SendForgotPasswordEmailAsync(string email, string fullName, string resetLink);
        Task SendEmailForNewStudyAsync(string email, string fullName);
        Task SendEmailSupportAsync(string userName, Support support);
    }
}
