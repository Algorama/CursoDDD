using Kernel.Domain.Model.Dtos;
using Kernel.Domain.Model.Settings;

namespace Kernel.Domain.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage message, SmtpSettings settings = null);
    }
}
