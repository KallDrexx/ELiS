using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
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

            string rawXml;
            var serializer = new XmlSerializer(typeof (LicenseDetails));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, details);
                stream.Position = 0;

                using (var streamReader = new StreamReader(stream))
                    rawXml = streamReader.ReadToEnd();
            }

            // Sign the xml
            var doc = new XmlDocument();
            TextReader reader = new StringReader(rawXml);
            doc.Load(reader);
            var signedXml = new SignedXml(doc);
            signedXml.SigningKey = _key;

            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(reference);

            signedXml.ComputeSignature();
            var signature = signedXml.GetXml();
            if (doc.DocumentElement != null)
                doc.DocumentElement.AppendChild(doc.ImportNode(signature, true));

            if (doc.FirstChild is XmlDeclaration)
                doc.RemoveChild(doc.FirstChild);

            // Return the resulting xml
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}
