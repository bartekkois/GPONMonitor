using AutoMapper;
using GPONMonitor.Models.Onu;
using GPONMonitor.Services;

namespace GPONMonitor.Models.OnuFactory
{
    public class UnknownOnuFactory : OnuFactory
    {
        public UnknownOnuFactory(IResponseDescriptionDictionaries responseDescriptionDictionaries, IMapper mapper, IDataService snmpDataService) : base(responseDescriptionDictionaries, mapper, snmpDataService) { }

        public override IOnuFactory BuildOnu(uint oltId, uint oltPortId, uint onuId)
        {
            UnknownOnu onu = _mapper.Map<UnknownOnu>(base.BuildOnu(oltId, oltPortId, onuId));
            return onu;
        }
    }
}
