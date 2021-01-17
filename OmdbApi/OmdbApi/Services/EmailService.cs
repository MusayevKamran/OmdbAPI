using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using OmdbApi.Configurations;
using OmdbApi.Interfaces;

namespace OmdbApi.Services
{
    public class EmailService : IEmail
    {
        private readonly IOptions<EmailConfiguration> _emailConfiguration;
        
        public EmailService(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var from = _emailConfiguration.Value.From;
            var password = _emailConfiguration.Value.Password;
            var senderName = _emailConfiguration.Value.SenderName;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(from, password);

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
