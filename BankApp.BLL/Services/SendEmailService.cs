using BankApp.BLL.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of email service
    /// </summary>
    public class SendEmailService : ISendEmailService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILogger<SendEmailService> logger;

        public SendEmailService(ILogger<SendEmailService> logger)
        {
            if(logger == null)
            {
                throw new ArgumentNullException(nameof(logger), " was null.");
            }

            this.logger = logger;
        }

        /// <summary>
        /// Send email message
        /// </summary>
        public async Task SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Bank Administration", "banksite2020@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("banksite2020@gmail.com", "Bank_microsoft2020");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);

                logger.LogInformation("Message was send");
            }
        }
    }
}
