using System;

namespace Elis.Client
{
    [Serializable]
    public class LicenseDetails
    {
        public string LicenseKey { get; set; }
        public string Application { get; set; }
        public SerializableVersion MinVersion { get; set; }
        public SerializableVersion MaxVersion { get; set; }
        public string LicensedUserName { get; set; }
        public string LicensedUserEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SerializableDictionary<string, string> CustomValues { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((LicenseDetails) obj);
        }

        private bool Equals(LicenseDetails other)
        {
            return string.Equals(LicenseKey, other.LicenseKey) && string.Equals(Application, other.Application) &&
                   Equals(MinVersion, other.MinVersion) && Equals(MaxVersion, other.MaxVersion) &&
                   string.Equals(LicensedUserName, other.LicensedUserName) &&
                   string.Equals(LicensedUserEmail, other.LicensedUserEmail) && StartDate.Equals(other.StartDate) &&
                   EndDate.Equals(other.EndDate) && Equals(CustomValues, other.CustomValues);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (LicenseKey != null ? LicenseKey.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Application != null ? Application.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MinVersion != null ? MinVersion.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MaxVersion != null ? MaxVersion.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LicensedUserName != null ? LicensedUserName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LicensedUserEmail != null ? LicensedUserEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (CustomValues != null ? CustomValues.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}