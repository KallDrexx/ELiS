﻿using System;
using NUnit.Framework;

namespace Elis.Client.Tests
{
    [TestFixture]
    class GeneralTests
    {
        [Test]
        public void TwoLicenseDetailsWithSameValuesAreEqual()
        {
            var lic1 = CreateLicenseDetail();
            var lic2 = CreateLicenseDetail();
            
            Assert.IsTrue(lic1.Equals(lic2), "Licenses were not equal");
        }

        [Test]
        public void TwoLicensesWithDifferentDictionaryValuesAreNotEqual()
        {
            var lic1 = CreateLicenseDetail();
            var lic2 = CreateLicenseDetail();
            lic2.CustomValues["key2"] = "val3";

            Assert.IsFalse(lic1.Equals(lic2), "Licenses were incorrectly marked as equal");
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
