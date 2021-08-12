using Kernel.Domain.Model.Dtos;
using Kernel.Domain.Model.Settings;
using Kernel.Domain.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Kernel.Infra.Email
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Send(EmailMessage message, SmtpSettings settings = null)
        {
            if (settings == null)
                settings = _appSettings.SmtpSettings;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(settings.User), 
                Subject = message.Subject, 
                IsBodyHtml = message.IsHtml, 
                Body = message.Body 
            };

            AddAddresses(mailMessage.To, message.To);
            AddAddresses(mailMessage.CC, message.Cc);
            AddAddresses(mailMessage.Bcc, message.Bcc);

            if (message.Attachments != null)
            {
                foreach (var att in message.Attachments)
                {
                    if (att != null && att.Content != null && !string.IsNullOrWhiteSpace(att.Name))
                        mailMessage.Attachments.Add(new Attachment(att.Content, att.Name));
                }
            }

            var client = new SmtpClient(settings.Server)
            {
                Port = settings.Port,
                EnableSsl = settings.Ssl,
                UseDefaultCredentials = settings.UseDefaultCredentials,
                Credentials = new NetworkCredential(settings.User, settings.Password)
            };

            client.Send(mailMessage);
        }

        private static void AddAddresses(MailAddressCollection collection, IEnumerable<string> addresses)
        {
            if (collection == null || addresses == null)
                return;

            foreach (var address in addresses)
            {
                if (!string.IsNullOrEmpty(address))
                    collection.Add(new MailAddress(address.Trim()));
            }
        }
    }
}