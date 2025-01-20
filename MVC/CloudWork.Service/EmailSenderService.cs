﻿using Microsoft.Extensions.Configuration;
using CloudWork.Service.Interface;
using CloudWork.Common;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MailKit.Security;
using CloudWork.Model;
namespace CloudWork.Service
{
    [Service]
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 发送邮件，从appsettings.json配置邮件参数
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="Body"></param>
        /// <param name="IsBodyHtml"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            string? MailServer = _configuration["EmailSettings:MailServer"];
            string FromEmail = _configuration["EmailSettings:FromEmail"] ?? string.Empty;
            string? Password = _configuration["EmailSettings:Password"];
            string? SenderName = _configuration["EmailSettings:SenderName"];
            int Port = Convert.ToInt32(_configuration["EmailSettings:Port"]);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(SenderName, FromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(MailServer, Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(FromEmail, Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            _logger.LogInformation($"Email sent to {toEmail}");
        }

        public Task SendConfirmationEmailAsync(string toEmail, string? userName, string safeLink)
        {
            var htmlMessage = $@"
                <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
                    <p>你好 {userName} ,</p>
                    <p>感谢您创建账户 <strong>CloudWork</strong>.
                    为安全使用全部功能，请访问链接以确认邮箱：</p>
                    <p>
                        <a href=""{safeLink}"" 
                            style=""background-color:#007bff;color:#fff;padding:10px 20px;text-decoration:none;
                                    font-weight:bold;border-radius:5px;display:inline-block;"">
                            Confirm Email
                        </a>
                    </p>
                    <p>如果点击链接无效，请复制并粘贴以下链接到浏览器地址栏：</p>
                        <br />
                        <a href=""{safeLink}"" style=""color:#007bff;text-decoration:none;"">{safeLink}</a>
                    </p>
                    <p>如果您没有进行此操作，请忽略此邮件。</p>
                    <p>Thanks,<br />
                    The CloudWork Team</p>
                </div>
            ";

            var subject = "邮箱确认";

            return SendEmailAsync(toEmail, subject, htmlMessage);
        }

        public Task SendForgotPasswordEmailAsync(string toEmail, string? userName, string safeLink)
        {
            var htmlMessage = $@"
                <div style=""font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #333; line-height: 1.5; padding: 20px;"">
                    <h2 style=""color: #007bff; text-align: center;"">Password Reset Request</h2>
                    <p style=""margin-bottom: 20px;"">你好 {userName}</p>
                    <p>We received a request to reset your password for your <strong>Dot Net Tutorials</strong> account. If you made this request, please click the button below to reset your password:</p>
                    <div style=""text-align: center; margin: 20px 0;"">
                        <a href=""{safeLink}"" 
                           style=""background-color: #007bff; color: #fff; padding: 10px 20px; text-decoration: none; font-weight: bold; border-radius: 5px; display: inline-block;"">
                            Reset Password
                        </a>
                    </div>
                    <p>如果点击链接无效，请复制并粘贴以下链接到浏览器地址栏：</p>
                    <p style=""background-color: #f8f9fa; padding: 10px; border: 1px solid #ddd; border-radius: 5px;"">
                        <a href=""{safeLink}"" style=""color: #007bff; text-decoration: none;"">{safeLink}</a>
                    </p>
                    <p>如果您没有进行此操作，请忽略此邮件。</p>
                    <p>Thanks,<br />
                    The CloudWork Team</p>
                </div>";

            var subject = "密码重置";

            return SendEmailAsync(toEmail, subject, htmlMessage);
        }

    }
}
