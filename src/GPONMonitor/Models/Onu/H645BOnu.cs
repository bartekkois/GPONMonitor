using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.OnuFactory;

namespace GPONMonitor.Models.Onu
{
    public class H645BOnu : GenericOnu, IOnuFactory
    {
        public ComplexIntType EthernetPort1State { get; set; }
        public ComplexIntType EthernetPort1Speed { get; set; }
        public ComplexIntType EthernetPort2State { get; set; }
        public ComplexIntType EthernetPort2Speed { get; set; }
    }
}
