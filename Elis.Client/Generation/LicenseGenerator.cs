using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Elis.Client.Generation
{
    public class LicenseGenerator
    {
        private readonly RSACryptoServiceProvider _key;

        public LicenseGenerator(RSACryptoServiceProvider key)
        {
            _key = key;
        }

        public string GenerateSignedXml(LicenseDetails details)
        {
            if (details == null)
                throw new ArgumentNullException("details");

            var serializer = new XmlSerializer(typeof (LicenseDetails));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, details);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
    }
}
