using AutoMapper;
using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Olt;
using GPONMonitor.Models.Onu;
using GPONMonitor.Services;
using GPONMonitor.Utils;
using System;

namespace GPONMonitor.Models.OnuFactory
{
    public abstract class OnuFactory
    {
        internal readonly IResponseDescriptionDictionaries _responseDescriptionDictionaries;
        internal readonly IDataService _snmpDataService;
        internal readonly IMapper _mapper;

        public OnuFactory(IResponseDescriptionDictionaries responseDescriptionDictionaries, IMapper mapper, IDataService snmpDataService)
        {
            _responseDescriptionDictionaries = responseDescriptionDictionaries;
            _mapper = mapper;
            _snmpDataService = snmpDataService;
        }

        public virtual IOnuFactory BuildOnu(uint oltId, uint oltPortId, uint onuId)
        {
            IOnuFactory onu = new GenericOnu();

            onu.OltId = oltId;
            onu.OltPortId = oltPortId;
            onu.OltOnuId = onuId;

            // Model type
            string modelType = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDGetOnuModelType + "." + oltPortId + "." + onuId).Result;

            if (modelType != null)
                onu.ModelType = new ComplexStringType(modelType, modelType.ToString(), SeverityLevel.Default);
            else
                onu.ModelType = new ComplexStringType(null, null, SeverityLevel.Unknown);


            // Description name
            string descriptionName = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuDescription + "." + oltPortId + "." + onuId).Result;

            if (descriptionName != null)
                onu.DescriptionName = new ComplexStringType(descriptionName, descriptionName.ToString().Replace("_", " "), SeverityLevel.Default);
            else
                onu.DescriptionName = new ComplexStringType(null, null, SeverityLevel.Unknown);


            // GPON serial number
            string gponSerialNumber = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuGponSerialNumber + "." + oltPortId + "." + onuId).Result;

            if (gponSerialNumber != null)
                onu.GponSerialNumber = new ComplexStringType(gponSerialNumber, gponSerialNumber.ToString(), SeverityLevel.Default);
            else
                onu.GponSerialNumber = new ComplexStringType(null, null, SeverityLevel.Unknown);


            // GPON profile
            string gponProfile = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuGponProfile + "." + oltPortId + "." + onuId).Result;

            if (gponProfile != null)
                onu.GponProfile = new ComplexStringType(gponProfile, gponProfile.ToString(), SeverityLevel.Default);
            else
                onu.GponProfile = new ComplexStringType(null, null, SeverityLevel.Unknown);


            // Firmware version
            string firmwareVersion = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuFirmwareVersion + "." + oltPortId + "." + onuId).Result;

            if (firmwareVersion != null)
                onu.FirmwareVersion = new ComplexStringType(firmwareVersion, firmwareVersion.ToString(), SeverityLevel.Default);
            else
                onu.FirmwareVersion = new ComplexStringType(null, null, SeverityLevel.Unknown);


            // Optical connection state
            int? opticalConnectionState = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionState + "." + oltPortId + "." + onuId).Result;
            onu.OpticalConnectionState = new ComplexIntType(opticalConnectionState, _responseDescriptionDictionaries.OpticalConnectionStateResponse(opticalConnectionState));


            // Optical connection deactivation reason
            int? opticalConnectionDeactivationReason = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionDeactivationReason + "." + oltPortId + "." + onuId).Result;
            onu.OpticalConnectionDeactivationReason = new ComplexIntType(opticalConnectionDeactivationReason, _responseDescriptionDictionaries.OpticalConnectionDeactivationReasonResponse(opticalConnectionDeactivationReason));


            // Optical power received
            // ONU Optical Power Received
            // -400 - no signal (-40,0 dBm)
            // other value - dBm level
            string opticalPowerReceived = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalPowerReceived + "." + oltPortId + "." + onuId).Result;


            if (decimal.TryParse(opticalPowerReceived, out decimal parsedValue))
            {
                SeverityLevel severity;
                decimal calculateddBmPower = parsedValue / 10;

                if (calculateddBmPower < -9.0m && calculateddBmPower > -26.0m)
                    severity = SeverityLevel.Success;
                else if ((calculateddBmPower < -8.0m && calculateddBmPower >= -9.0m) || (calculateddBmPower <= -26.0m && calculateddBmPower > -27.0m))
                    severity = SeverityLevel.Warning;
                else
                    severity = SeverityLevel.Danger;

                onu.OpticalPowerReceived = new ComplexStringType(calculateddBmPower.ToString(), calculateddBmPower.ToString() + " dBm", severity);
            }
            else
            {
                onu.OpticalPowerReceived = new ComplexStringType(null, null, SeverityLevel.Unknown);
            }


            // Optical cable distance
            int? opticalCableDistance = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalCabelDistance + "." + oltPortId + "." + onuId).Result;
            if (opticalCableDistance != null)
                onu.OpticalCableDistance = new ComplexIntType(opticalCableDistance, opticalCableDistance.ToString() + " m", SeverityLevel.Default);
            else
                onu.OpticalCableDistance = new ComplexIntType(null, null, SeverityLevel.Unknown);


            // Optical connection uptime
            int? opticalConnectionUptime = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionUptime + "." + oltPortId + "." + onuId).Result;
            if (opticalConnectionUptime != null)
                onu.OpticalConnectionUptime = new ComplexIntType(opticalConnectionUptime, TimeSpanConverter.CalculateTimeSpanAndDateTimeFormSeconds(opticalConnectionUptime), SeverityLevel.Default);
            else
                onu.OpticalConnectionUptime = new ComplexIntType(null, null, SeverityLevel.Unknown);


            // Optical connection inactive time
            int? opticalConnectionInactiveTime = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionInactiveTime + "." + oltPortId + "." + onuId).Result;
            if (opticalConnectionInactiveTime != null)
                onu.OpticalConnectionInactiveTime = new ComplexIntType(opticalConnectionInactiveTime, TimeSpanConverter.CalculateTimeSpanAndDateTimeFormSeconds(opticalConnectionInactiveTime), SeverityLevel.Default);
            else
                onu.OpticalConnectionInactiveTime = new ComplexIntType(null, null, SeverityLevel.Unknown);


            // System uptime
            int? systemUptime = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuSystemUptime + "." + oltPortId + "." + onuId).Result;
            if (systemUptime != null)
                onu.SystemUptime = new ComplexIntType(systemUptime, TimeSpanConverter.CalculateTimeSpanAndDateTimeFormSeconds(systemUptime), SeverityLevel.Default);
            else
                onu.SystemUptime = new ComplexIntType(null, null, SeverityLevel.Unknown);


            // Block status
            int? blockStatus = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockStatus + "." + oltPortId + "." + onuId).Result;
            onu.BlockStatus = new ComplexIntType(blockStatus, _responseDescriptionDictionaries.BlockStatusResponse(blockStatus));


            // Block reason
            int? blockReason = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockReason + "." + oltPortId + "." + onuId).Result;
            onu.BlockReason = new ComplexIntType(blockReason, _responseDescriptionDictionaries.BlockReasonResponse(blockReason));

            return onu;
        }
    }
}
