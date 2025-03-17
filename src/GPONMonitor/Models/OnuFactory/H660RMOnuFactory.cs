using AutoMapper;
using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Olt;
using GPONMonitor.Models.Onu;
using GPONMonitor.Services;
using System.Threading.Tasks;

namespace GPONMonitor.Models.OnuFactory
{
    public class H660RMOnuFactory : OnuFactory
    {
        public H660RMOnuFactory(IResponseDescriptionDictionaries responseDescriptionDictionaries, IMapper mapper, IDataService snmpDataService) : base(responseDescriptionDictionaries, mapper, snmpDataService) { }

        public override IOnuFactory BuildOnu(uint oltId, uint oltPortId, uint onuId)
        {
            H660RMOnu onu = _mapper.Map<H660RMOnu>(base.BuildOnu(oltId, oltPortId, onuId));

            // Ethernet port 1 state
            int? ethernetPort1State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + onuId + ".1.1").Result;
            onu.EthernetPort1State = new ComplexIntType(ethernetPort1State, _responseDescriptionDictionaries.EthernetPortStateResponse(ethernetPort1State));


            // Ethernet port 1 speed
            int? ethernetPort1Speed = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + onuId + ".1.1").Result;
            onu.EthernetPort1Speed = new ComplexIntType(ethernetPort1Speed, _responseDescriptionDictionaries.EthernetPortSpeedResponse(ethernetPort1Speed));


            // Ethernet port 2 state
            int? ethernetPort2State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + onuId + ".1.2").Result;
            onu.EthernetPort2State = new ComplexIntType(ethernetPort2State, _responseDescriptionDictionaries.EthernetPortStateResponse(ethernetPort2State));


            // Ethernet port 2 speed
            int? ethernetPort2Speed = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + onuId + ".1.2").Result;
            onu.EthernetPort2Speed = new ComplexIntType(ethernetPort2Speed, _responseDescriptionDictionaries.EthernetPortSpeedResponse(ethernetPort2Speed));


            // Ethernet port 3 state
            int? ethernetPort3State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + onuId + ".1.3").Result;
            onu.EthernetPort3State = new ComplexIntType(ethernetPort3State, _responseDescriptionDictionaries.EthernetPortStateResponse(ethernetPort3State));


            // Ethernet port 3 speed
            int? ethernetPort3Speed = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + onuId + ".1.3").Result;
            onu.EthernetPort3Speed = new ComplexIntType(ethernetPort3Speed, _responseDescriptionDictionaries.EthernetPortSpeedResponse(ethernetPort3Speed));


            // Ethernet port 4 state
            int? ethernetPort4State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + onuId + ".1.4").Result;
            onu.EthernetPort4State = new ComplexIntType(ethernetPort4State, _responseDescriptionDictionaries.EthernetPortStateResponse(ethernetPort4State));


            // Ethernet port 4 speed
            int? ethernetPort4Speed = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + onuId + ".1.4").Result;
            onu.EthernetPort4Speed = new ComplexIntType(ethernetPort4Speed, _responseDescriptionDictionaries.EthernetPortSpeedResponse(ethernetPort4Speed));


            // Update VoIP data
            _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuVoIPLineStateUpdate1, 1);
            _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuVoIPLineStateUpdateOltPortId, (int)oltPortId);
            _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuVoIPLineStateUpdateOnuId, (int)onuId);
            _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuVoIPLineStateUpdate0, 0);
            Task.Delay(400).Wait();


            // VoIP port 1 state
            int? voipPort1State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuVoIPLineState + "." + oltPortId + "." + onuId + ".1").Result;
            onu.VoIPLine1State = new ComplexIntType(voipPort1State, _responseDescriptionDictionaries.VoIPLinestatusResponse(voipPort1State));


            // VoIP port 2 state
            int? voipPort2State = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuVoIPLineState + "." + oltPortId + "." + onuId + ".2").Result;
            onu.VoIPLine2State = new ComplexIntType(voipPort2State, _responseDescriptionDictionaries.VoIPLinestatusResponse(voipPort2State));

            return onu;
        }
    }
}