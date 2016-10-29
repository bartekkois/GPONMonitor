namespace GPONMonitor.Models.Onu
{
    public class UnknownOnu : OnuGeneric
    {
        public UnknownOnu(uint oltPortId, uint oltOnuId) : base(oltPortId, oltOnuId)
        {
        }
    }
}
