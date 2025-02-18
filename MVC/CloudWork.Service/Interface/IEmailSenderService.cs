﻿namespace CloudWork.Service.Interface
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
        Task SendConfirmationEmailAsync(string toEmail, string? userName, string safeLink);
        Task SendForgotPasswordEmailAsync(string toEmail, string? userName, string safeLink);
        Task SendPasswordChangedNotificationEmail(string email, string? userName, string location, string device);
    }
}
