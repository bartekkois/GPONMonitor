using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class UnknownOnu : OnuGeneric
    {
        public UnknownOnu(uint oltId, uint oltPortId, uint oltOnuId, IDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, snmpDataService)
        {
        }
    }
}
