namespace Kernel.Domain.Model.Settings
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}
