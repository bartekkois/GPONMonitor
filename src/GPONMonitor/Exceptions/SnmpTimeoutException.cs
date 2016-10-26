using System;

namespace GPONMonitor.Exceptions
{
    public class SnmpTimeoutException : Exception
    {
        public SnmpTimeoutException(string message) : base(message)
        {
            Source = "SnmpConnection";
            HelpLink = "https://github.com/bartekkois/GPONMonitor";
        }
    }
}
