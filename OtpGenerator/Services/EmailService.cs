using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
//using System.Net.Mail;
using MimeKit;
using OtpGenerator.Models;

namespace OtpGenerator.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to_email, string subject, string body)
        {
            Console.WriteLine("Send Email Async enter");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            Console.WriteLine(_emailSettings.FromName, _emailSettings.FromAddress);

            message.To.Add(new MailboxAddress("", to_email));
            Console.WriteLine(to_email);
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };
            Console.WriteLine(body);

            using (var client = new SmtpClient())
            {
                Console.WriteLine("before connectasync");
                client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                Console.WriteLine("before auth sync");
                client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                Console.WriteLine("before async send");
                client.SendAsync(message);
                Console.WriteLine("after async send");
                client.DisconnectAsync(true);
                Console.WriteLine("After disconnect");
            }
        }
    }
}
