using GPONMonitor.Models.ComplexStateTypes;

namespace GPONMonitor.Models.OnuFactory
{
    public interface IOnuFactory
    {
        uint OltId { get; set; }
        uint OltPortId { get; set; }
        uint OltOnuId { get; set; }

        ComplexStringType ModelType { get; set; }
        ComplexStringType DescriptionName { get; set; }
        ComplexStringType GponSerialNumber { get; set; }
        ComplexStringType GponProfile { get; set; }
        ComplexStringType FirmwareVersion { get; set; }

        ComplexIntType OpticalConnectionState { get; set; }
        ComplexIntType OpticalConnectionDeactivationReason { get; set; }
        ComplexStringType OpticalPowerReceived { get; set; }
        ComplexIntType OpticalCableDistance { get; set; }

        ComplexIntType OpticalConnectionUptime { get; set; }
        ComplexIntType OpticalConnectionInactiveTime { get; set; }
        ComplexIntType SystemUptime { get; set; }

        ComplexIntType BlockStatus { get; set; }
        ComplexIntType BlockReason { get; set; }
    }
}
