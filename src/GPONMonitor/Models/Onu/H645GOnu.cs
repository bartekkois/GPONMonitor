using GPONMonitor.Models.ComplexStateTypes;

namespace GPONMonitor.Models.Onu
{
    public class H645GOnu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }

        public H645GOnu(uint oltId, uint oltPortId, uint oltOnuId) : base(oltId, oltPortId, oltOnuId)
        {
        }
    }
}
