using System;

namespace Elis.Client.Exceptions
{
    public class LicenseSignatureMismatchException : InvalidOperationException
    {
        public LicenseSignatureMismatchException()
            : base("The license's xml signature did not match its content")
        {
            
        }
    }
}
