namespace Kernel.Domain.Model.Settings
{
    public class StorageSettings
    {
        public string ConnectionString { get; set; }
        public string UserPhotoContainer { get; set; }
        public int SasUriExpirantionHours { get; set; }
    }
}
