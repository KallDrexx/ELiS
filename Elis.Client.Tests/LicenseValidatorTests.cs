using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Elis.Client.Exceptions;
using Elis.Client.Generation;
using Elis.Client.Validation;
using NUnit.Framework;

namespace Elis.Client.Tests
{
    [TestFixture]
    class LicenseValidatorTests
    {
        [Test]
        public void GeneratedXmlCanBeValidated()
        {
            // Setup
            var license = CreateLicenseDetail();
            var key = new RSACryptoServiceProvider(1024);
            var xml = new LicenseGenerator(key).GenerateSignedXml(license);

            // Test
            var validator = new LicenseValidator(key);
            var validatedLicense = validator.ValidateLicenseXml(xml);

            // Verify
            Assert.IsNotNull(validatedLicense, "Validated license was null");
            Assert.IsTrue(license.Equals(validatedLicense), "Validated license did not match original license");
        }

        [Test]
        [ExpectedException(typeof(LicenseSignatureMissingException))]
        public void ThrowsExceptionWhenSignatureNotPresent()
        {
            string xml;

            // Serialize license to xml manually without signature to verify
            var license = CreateLicenseDetail();
            var key = new RSACryptoServiceProvider(1024);
            var serializer = new XmlSerializer(typeof(LicenseDetails));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, license);
                stream.Position = 0;

                using (var streamReader = new StreamReader(stream))
                    xml = streamReader.ReadToEnd();
            }

            // Test
            new LicenseValidator(key).ValidateLicenseXml(xml);
        }

        [Test]
        [ExpectedException(typeof(LicenseSignatureMismatchException))]
        public void ThrowsExceptionWhenSignatureIsNotValid()
        {
            // Setup
            var license = CreateLicenseDetail();
            var key = new RSACryptoServiceProvider(1024);
            var xml = new LicenseGenerator(key).GenerateSignedXml(license);
            xml = xml.Replace("<LicenseKey>1234</LicenseKey>", "<LicenseKey>12345</LicenseKey>");

            // Test
            new LicenseValidator(key).ValidateLicenseXml(xml);
        }

        [Test]
        [ExpectedException(typeof(InvalidLicenseXmlException))]
        public void ThrowsExceptionWhenInvalidXmlIsValidated()
        {
            var key = new RSACryptoServiceProvider(1024);
            var xml = "test 1234";
            new LicenseValidator(key).ValidateLicenseXml(xml);
        }

        private LicenseDetails CreateLicenseDetail()
        {
            return new LicenseDetails
            {
                Application = "abc",
                StartDate = new DateTime(2000, 1, 2),
                EndDate = new DateTime(2000, 1, 3),
                MinVersion = new SerializableVersion(1, 2, 3, 4),
                MaxVersion = new SerializableVersion(2, 3, 4, 5),
                LicenseKey = "1234",
                LicensedUserName = "me",
                CustomValues = new SerializableDictionary<string, string>
                {
                    {"key1", "val1"},
                    {"key2", "val2"}
                }
            };
        }
    }
}
