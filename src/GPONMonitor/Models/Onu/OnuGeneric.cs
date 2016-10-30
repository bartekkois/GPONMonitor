using GPONMonitor.Models.ComplexStateTypes;

namespace GPONMonitor.Models.Onu
{
    public abstract class OnuGeneric
    {
        public uint OltId { get; private set; }
        public uint OltPortId { get; private set; }
        public uint OltOnuId { get; private set; }
        public ModelType ModelType { get; private set; }
        public Description Description { get; private set; }
        public GponSerialNumber GponSerialNumber { get; private set; }

        public OpticalConnectionState OpticalConnectionState { get; private set; }
        public OpticalConnectionDeactivationReason OpticalConnectionDeactivationReason { get; private set; }
        public OpticalPowerReceived OpticalPowerReceived { get; private set; }
        public OpticalCableDistance OpticalCableDistance { get; private set; }

        public OpticalConnectionUptime OpticalConnectionUptime { get; private set; }
        public OpticalConnectionInactiveTime OpticalConnectionInactiveTime{ get; private set; }
        public SystemUptime SystemUptime { get; private set; }

        public BlockStatus BlockStatus { get; private set; }
        public BlockReason BlockReason { get; private set; }


        public OnuGeneric(uint oltId, uint oltPortId, uint oltOnuId)
        {
            OltId = oltId;
            OltPortId = oltPortId;
            OltOnuId = oltOnuId;
        }
    }
}