using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Services
{
    public interface IMailer
    {
        public Task SendEmailAsync(string email, string subject, string body);
        public Task SendVerificationLink(string email, string link);
    }

    public class Mailer : IMailer
    {
        private readonly SmtpSettings smtpSettings;

        public Mailer(IOptions<SmtpSettings> _smtpSettings)
        {
            smtpSettings = _smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {

                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task SendVerificationLink(string email, string link)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Verify your account";
                message.Body = new TextPart("html")
                {
                    Text = $"<a href='{ link }'>Click here to confirm your account</a>"
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {

                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
