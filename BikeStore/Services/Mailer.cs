using BikeStore.Models.Cart;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeStore.Services
{
    public interface IMailer
    {
        public Task SendEmailAsync(string email, string subject, string body);
        public Task SendVerificationLink(string email, string link);
        public Task SendOrderDetails(string email, List<Item> cart);
        public Task SendNews(List<string> emails, string news);
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
                message.Subject = "Підтвердіть ваш аккаунт";
                message.Body = new TextPart("html")
                {
                    Text = $"<a href='{ link }'>Натисніть тут, щоб підтвердити свій аккаунт</a>"
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

        public async Task SendOrderDetails(string email, List<Item> cart)
        {
            string content = "";
            double total = 0;
            foreach (var item in cart)
            {
                total += item.Quantity * item.Model.Price;
                content += $"" +
                    $"<tr>" +
                    $"<td style = 'border: solid thin;' align='center' class='text-center'>{item.Quantity}</td>" +
                    $"<td style = 'border: solid thin;' align='center'><img src = '{item.ModelColour.ImageLink}' style='width: 20%;' class='card-img-top'/></td>" +
                    $"<td style = 'border: solid thin;' align='center' class='text-left'>{item.Model.FullName}</td>" +
                    $"<td style = 'border: solid thin;' align='center' class='text-center'>{item.FrameSize.Size}</td>" +
                    $"<td style = 'border: solid thin;' align='center' class='text-right'>{item.Model.Price:c}</td>" +
                    $"<td style = 'border: solid thin;' align='center' class='text-right'>{(item.Quantity * item.Model.Price):c}</td>" +
                    $"</tr>";
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Ваш заказ у магазині Bike Store";
                message.Body = new TextPart("html")
                {
                    Text = $"<!DOCTYPE html>" +
                    $"<html>" +
                    $"<head>" +
                    $"<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css' integrity='sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh' crossorigin='anonymous'>" +
                    $"</head>" +
                    $"<body style='background-color: black;'>" +
                    $"<div class='container'>" +
                        $"<table style='font-size:15px; border-collapse: collapse; color: white;'>" +
                        $"<thead>" +
                            $"<tr> " +
                                $"<th style = 'border: solid thin;' align='center'> Кількість </th>" +
                                $"<td style = 'border: solid thin;' align='center'> Зображення </td>" +
                                $"<th style = 'border: solid thin;' align='center'> Товар </th>" +
                                $"<th style = 'border: solid thin;' align='center'> Розмір </th>" +
                                $"<th style = 'border: solid thin;' align='center' class='text-right'>Ціна</th>" +
                                $"<th style = 'border: solid thin;' align='center' class='text-right'>Разом</th>" +
                            $"</tr>" +
                        $"</thead>" +
                        $"<tbody>" +
                        content +
                        $"</tbody>" +
                        $"<tfoot>" +
                        $"<tr>" +
                        $"<td style = 'border: solid thin;' align='center' colspan = '3' class='text-right'>Загалом:</td>" +
                        $"<td style = 'border: solid thin;' align='center' class='text-right'>{total:c}</td>" +
                        $"</tr>" +
                        $"</tfoot>" +
                        $"</table>" +
                    $"</div>" +
                    $"</body>" +
                    $"</html>"
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

        public async Task SendNews(List<string> emails, string news)
        {
            foreach (string email in emails)
            {
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                    message.To.Add(new MailboxAddress("", email));
                    message.Subject = "Новини Bike Store";
                    message.Body = new TextPart("html")
                    {
                        Text = $"{news}"
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
}
