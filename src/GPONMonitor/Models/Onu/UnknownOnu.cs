using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class UnknownOnu : OnuGeneric
    {
        public UnknownOnu(uint oltId, uint oltPortId, uint oltOnuId, IResponseDescriptionDictionaries responseDescriptionDictionaries, IDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, responseDescriptionDictionaries, snmpDataService)
        {
        }
    }
}
