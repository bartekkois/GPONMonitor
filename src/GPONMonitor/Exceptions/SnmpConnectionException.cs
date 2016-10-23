using System;

namespace GPONMonitor.Exceptions
{
    public class SnmpConnectionException : Exception
    {
        public SnmpConnectionException(string message) : base(message)
        {
            Source = "SnmpConnection";
            HelpLink = "https://github.com/bartekkois/GPONMonitor";
        }
    }
}
