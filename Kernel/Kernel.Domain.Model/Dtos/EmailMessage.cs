using System.Collections.Generic;
using System.IO;

namespace Kernel.Domain.Model.Dtos
{
    public class EmailMessage
    {
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public List<EmailMessageAttachment> Attachments { get; set; }

        public EmailMessage(string to, string subject, string body, bool isHtml = true)
        {
            To = new List<string> { to };
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            Cc = new List<string>();
            Bcc = new List<string>();
            Attachments = new List<EmailMessageAttachment>();            
        }

        public void AddAttachment(string name, Stream content)
        {
            if (Attachments == null)
                Attachments = new List<EmailMessageAttachment>();

            Attachments.Add(new EmailMessageAttachment { Name = name, Content = content });
        }
    }

    public class EmailMessageAttachment
    {
        public string Name { get; set; }
        public Stream Content { get; set; }
    }
}
