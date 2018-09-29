using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.OnuFactory;

namespace GPONMonitor.Models.Onu
{
    public class GenericOnu : IOnuFactory
    {
        public uint OltId { get; set; }
        public uint OltPortId { get; set; }
        public uint OltOnuId { get; set; }

        public ComplexStringType ModelType { get; set; }
        public ComplexStringType DescriptionName { get; set; }
        public ComplexStringType GponSerialNumber { get; set; }
        public ComplexStringType GponProfile { get; set; }
        public ComplexStringType FirmwareVersion { get; set; }

        public ComplexIntType OpticalConnectionState { get; set; }
        public ComplexIntType OpticalConnectionDeactivationReason { get; set; }
        public ComplexStringType OpticalPowerReceived { get; set; }
        public ComplexIntType OpticalCableDistance { get; set; }

        public ComplexIntType OpticalConnectionUptime { get; set; }
        public ComplexIntType OpticalConnectionInactiveTime { get; set; }
        public ComplexIntType SystemUptime { get; set; }

        public ComplexIntType BlockStatus { get; set; }
        public ComplexIntType BlockReason { get; set; }
    }
}