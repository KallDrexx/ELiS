using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Serialization;
using Elis.Client.Exceptions;

namespace Elis.Client.Validation
{
    public class LicenseValidator
    {
        private RSACryptoServiceProvider _key;

        public LicenseValidator(RSACryptoServiceProvider key)
        {
            _key = key;
        }

        public LicenseDetails ValidateLicenseXml(string xml)
        {
            var doc = new XmlDocument();
            using (TextReader reader = new StringReader(xml))
            {
                try
                {
                    doc.Load(reader);
                }
                catch
                {
                    throw new InvalidLicenseXmlException();
                }

                // Validate the xml's signature
                var signedXml = new SignedXml(doc);
                var nodeList = doc.GetElementsByTagName("Signature");
                if (nodeList.Count == 0)
                    throw new LicenseSignatureMissingException();

                signedXml.LoadXml((XmlElement) nodeList[0]);
                if (!signedXml.CheckSignature(_key))
                    throw new LicenseSignatureMismatchException();
            }

            // Deserialize the xml
            var deserializer = new XmlSerializer(typeof(LicenseDetails));
            using (TextReader reader = new StringReader(xml))
                return (LicenseDetails) deserializer.Deserialize(reader);
        }
    }
}
