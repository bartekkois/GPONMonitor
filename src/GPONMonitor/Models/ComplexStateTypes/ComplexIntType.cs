namespace GPONMonitor.Models.ComplexStateTypes
{
    public class ComplexIntType : ComplexType
    {
        public int? Value { get; private set; }

        public ComplexIntType(int? value, string description, SeverityLevel severity) : base (description, severity)
        {
            Value = value;
        }

        public ComplexIntType(int? value, ResponseDescription responseDescription) : base(responseDescription)
        {
            Value = value;
        }
    }
}
