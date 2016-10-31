using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class UnknownOnu : OnuGeneric
    {
        public UnknownOnu(uint oltId, uint oltPortId, uint oltOnuId, ISnmpDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, snmpDataService)
        {
        }
    }
}
