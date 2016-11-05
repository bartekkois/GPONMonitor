using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class BlockStatus
    {
        private int? value;
        public int? Value
        {
            get
            {
                return value;
            }
            set
            {
                ResponseDescription responseDescription;

                if (BlockStatusResponseDictionary.ContainsKey(value))
                {
                    BlockStatusResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    BlockStatusResponseDictionary.TryGetValue(null, out responseDescription);
                }

                DescriptionEng = responseDescription.DescriptionEng;
                DescriptionPol = responseDescription.DescriptionPol;
                Severity = responseDescription.Severity;
                this.value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }


        // ONT Block Status
        // 1 - autoblock
        // 2 - manual block
        // 255 - unblock

        readonly Dictionary<int?, ResponseDescription> BlockStatusResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 1, new ResponseDescription("autoblock", "blokada automatyczna", SeverityLevel.Danger) },
            { 2, new ResponseDescription("manual block", "blokada ręczna", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unblock", "brak blokady", SeverityLevel.Success) }
        };
    }
}