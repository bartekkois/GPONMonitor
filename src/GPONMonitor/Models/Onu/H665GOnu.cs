using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.OnuFactory;

namespace GPONMonitor.Models.Onu
{
    public class H665GOnu : GenericOnu, IOnuFactory
    {
        public ComplexIntType EthernetPort1State { get; set; }
        public ComplexIntType EthernetPort1Speed { get; set; }
    }
}
