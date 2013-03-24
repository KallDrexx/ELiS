using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Elis.Client.Generation;
using NUnit.Framework;

namespace Elis.Client.Tests
{
    [TestFixture]
    class LicenseGeneratorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionThrownWhenNullLicenseDetailsPassedIn()
        {
            var key = new RSACryptoServiceProvider();
            var generator = new LicenseGenerator(key);
            generator.GenerateSignedXml(null);
        }

        [Test]
        public void GeneratedXmlIsSerializableToLicenseDetails()
        {
            // Setup
            var key = new RSACryptoServiceProvider();
            var generator = new LicenseGenerator(key);
            var testLicense = new LicenseDetails
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                Application = "Test App",
                MinVersion = new SerializableVersion(1, 2, 3, 4),
                MaxVersion = new SerializableVersion(5, 6, 7, 8),
                LicensedTo = "me",
                LicenseKey = "1234",
                CustomValues = new SerializableDictionary<string, string>
                {
                    {"Key1", "val2"},
                    {"Key2", "Val2"}
                }
            };

            // Test
            var rawXml = generator.GenerateSignedXml(testLicense);

            // Verify
            LicenseDetails verificationLicense;
            Assert.IsNotNullOrEmpty(rawXml, "Null or empty xml returned");

            var deserializer = new XmlSerializer(typeof (LicenseDetails));

            using (TextReader reader = new StringReader(rawXml))
                verificationLicense = (LicenseDetails) deserializer.Deserialize(reader);

            Assert.IsTrue(testLicense.Equals(verificationLicense), "Licenses were not equal");
        }
    }
}
