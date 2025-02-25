using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.OnuFactory;

namespace GPONMonitor.Models.Onu
{
    public class HL1BOnu : GenericOnu, IOnuFactory
    {
        public ComplexIntType EthernetPort1State { get; set; }
        public ComplexIntType EthernetPort1Speed { get; set; }
    }
}
