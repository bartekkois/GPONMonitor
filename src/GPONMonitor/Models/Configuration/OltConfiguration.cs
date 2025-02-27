namespace GPONMonitor.Models.Configuration
{
    public class OltConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string SnmpPort { get; set; }
        public string SnmpVersion { get; set; }
        public string SnmpCommunity { get; set; }
        public string SnmpTimeout { get; set; }
        public string CommandProtectionPassword { get; set; }
        public SnmpV3Credentials SnmpV3Credentials { get; set; }
        public string IpHostWebManagementPort { get; set; }
    }
}
