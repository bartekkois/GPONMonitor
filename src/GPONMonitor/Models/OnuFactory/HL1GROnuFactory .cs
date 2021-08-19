using AutoMapper;
using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Olt;
using GPONMonitor.Models.Onu;
using GPONMonitor.Services;

namespace GPONMonitor.Models.OnuFactory
{
    public class HL1GROnuFactory : OnuFactory
    {
        public HL1GROnuFactory(IResponseDescriptionDictionaries responseDescriptionDictionaries, IMapper mapper, IDataService snmpDataService) : base(responseDescriptionDictionaries, mapper, snmpDataService) { }

        public override IOnuFactory BuildOnu(uint oltId, uint oltPortId, uint onuId)
        {
            HL1GROnu onu = _mapper.Map<HL1GROnu>(base.BuildOnu(oltId, oltPortId, onuId));

            // Ethernet port 1 state
            int? ethernetPort1State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + onuId + ".1.1").Result;
            onu.EthernetPort1State = new ComplexIntType(ethernetPort1State, _responseDescriptionDictionaries.EthernetPortStateResponse(ethernetPort1State));


            // Ethernet port 1 speed
            int? ethernetPort1Speed = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + onuId + ".1.1").Result;
            onu.EthernetPort1Speed = new ComplexIntType(ethernetPort1Speed, _responseDescriptionDictionaries.EthernetPortSpeedResponse(ethernetPort1Speed));

            return onu;
        }
    }
}