using AutoMapper;
using GPONMonitor.Models.Onu;
using GPONMonitor.Services;

namespace GPONMonitor.Models.OnuFactory
{
    public class HLGSFPOnuFactory : OnuFactory
    {
        public HLGSFPOnuFactory(IResponseDescriptionDictionaries responseDescriptionDictionaries, IMapper mapper, IDataService snmpDataService) : base(responseDescriptionDictionaries, mapper, snmpDataService) { }

        public override IOnuFactory BuildOnu(uint oltId, uint oltPortId, uint onuId)
        {
            HLGSFPOnu onu = _mapper.Map<HLGSFPOnu>(base.BuildOnu(oltId, oltPortId, onuId));

            return onu;
        }
    }
}