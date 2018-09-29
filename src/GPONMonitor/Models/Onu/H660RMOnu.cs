using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.OnuFactory;

namespace GPONMonitor.Models.Onu
{
    public class H660RMOnu : GenericOnu, IOnuFactory
    {
        public ComplexIntType EthernetPort1State { get; set; }
        public ComplexIntType EthernetPort1Speed { get; set; }
        public ComplexIntType EthernetPort2State { get; set; }
        public ComplexIntType EthernetPort2Speed { get; set; }
        public ComplexIntType EthernetPort3State { get; set; }
        public ComplexIntType EthernetPort3Speed { get; set; }
        public ComplexIntType EthernetPort4State { get; set; }
        public ComplexIntType EthernetPort4Speed { get; set; }

        public ComplexIntType VoIPLine1State { get; set; }
        public ComplexIntType VoIPLine2State { get; set; }
    }
}
