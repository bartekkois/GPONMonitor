﻿namespace GPONMonitor.Models.Onu
{
    public class UnknownOnu : OnuGeneric
    {
        public UnknownOnu(uint oltId, uint oltPortId, uint oltOnuId) : base(oltId, oltPortId, oltOnuId)
        {
        }
    }
}
