using System;

namespace Elis.Client.Exceptions
{
    public class InvalidLicenseXmlException : InvalidOperationException
    {
        public InvalidLicenseXmlException()
            : base("License xml is not valid xml")
        {
            
        }
    }
}
