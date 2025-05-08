using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
namespace api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpEmailAsync(string toEmail, string otp)
        {
            var smtpServer = _config["Email:SmtpServer"];
            var port = int.Parse(_config["Email:Port"]);
            var username = _config["Email:Username"];
            var password = _config["Email:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HR System", username));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Mã OTP đặt lại mật khẩu";

            message.Body = new TextPart("plain")
            {
                Text = $"Mã OTP của bạn là: {otp}\nMã này sẽ hết hạn sau 5 phút."
            };

            using SmtpClient? client = new SmtpClient();
            await client.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}