using System;

namespace Elis.Client.Exceptions
{
    public class LicenseSignatureMissingException : InvalidOperationException
    {
        public LicenseSignatureMissingException() 
            : base("License's xml signature is not present")
        {
            
        }
    }
}
