using GPONMonitor.Services;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class BlockReason
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

                ResponseDescription responseDescription = _responseDescriptionDictionaries.BlockReasonResponse(value.Value);
                DescriptionEng = responseDescription.DescriptionEng;
                DescriptionPol = responseDescription.DescriptionPol;
                Severity = responseDescription.Severity;
                this.value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        private readonly IResponseDescriptionDictionaries _responseDescriptionDictionaries;

        public BlockReason(IResponseDescriptionDictionaries responseDescriptionDictionaries)
        {
            _responseDescriptionDictionaries = responseDescriptionDictionaries;
        }
    }
}