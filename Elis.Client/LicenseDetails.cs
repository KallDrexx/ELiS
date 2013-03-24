using System;
using System.Linq;

namespace Elis.Client
{
    [Serializable]
    public class LicenseDetails
    {
        public string LicenseKey { get; set; }
        public string Application { get; set; }
        public SerializableVersion MinVersion { get; set; }
        public SerializableVersion MaxVersion { get; set; }
        public string LicensedTo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SerializableDictionary<string, string> CustomValues { get; set; }

        public override bool Equals(object obj)
        {
            var license = obj as LicenseDetails;
            if (license == null)
                return false;

            var propertysEqual = LicenseKey == license.LicenseKey &&
                                 Application == license.Application &&
                                 MinVersion == license.MinVersion &&
                                 MaxVersion == license.MaxVersion &&
                                 LicensedTo == license.LicensedTo &&
                                 StartDate == license.StartDate &&
                                 EndDate == license.EndDate;

            if (!propertysEqual)
                return false;

            // Compare the dictionaries
            foreach (var key in CustomValues.Keys )
            {
                if (!license.CustomValues.ContainsKey(key))
                    return false;

                if (!license.CustomValues[key].Equals(CustomValues[key]))
                    return false;
            }

            // Make sure the 2nd dictionary doesn't have any keys this license doesn't
            if (license.CustomValues.Keys.Any(x => !CustomValues.Keys.Contains(x)))
                return false;

            // All checks passed
            return true;
        }
    }
}