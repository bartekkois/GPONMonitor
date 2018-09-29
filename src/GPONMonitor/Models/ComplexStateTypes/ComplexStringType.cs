namespace GPONMonitor.Models.ComplexStateTypes
{
    public class ComplexStringType : ComplexType
    {
        public string Value { get; private set; }

        public ComplexStringType(string value, string description, SeverityLevel severity) : base(description, severity)
        {
            Value = value;
        }

        public ComplexStringType(string value, ResponseDescription responseDescription) : base(responseDescription)
        {
            Value = value;
        }
    }
}
