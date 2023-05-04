namespace GPONMonitor.Models.Configuration
{
    public class SnmpV3Credentials
    {
        public string AuthenticationUsername { get; set; }
        public string AuthenticationType { get; set; }
        public string AuthenticationPassword { get; set; }
        public string EncryptionType { get; set; }
        public string EncryptionPassword { get; set; }
    }
}