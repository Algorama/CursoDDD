namespace Kernel.Domain.Model.Settings
{
    public class TokenSettings
    {
        public int TokenExpirationInHours { get; set; }
        public string TokenSecretKey { get; set; }
    }
}