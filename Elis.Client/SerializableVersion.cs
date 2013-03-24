using System;
using System.Globalization;

namespace Elis.Client
{
    /// <summary>
    /// Serializable version of the System.Version class.
    /// Code originally from http://stackoverflow.com/a/2085890/231002
    /// </summary>
    [Serializable]
    public class SerializableVersion : ICloneable, IComparable
    {
        private int _major;
        private int _minor;
        private int _build;
        private int _revision;

        /// <summary>
        /// Gets the major.
        /// </summary>
        /// <value></value>
        public int Major
        {
            get
            {
                return _major;
            }
            set
            {
                _major = value;
            }
        }

        /// <summary>
        /// Gets the minor.
        /// </summary>
        /// <value></value>
        public int Minor
        {
            get
            {
                return _minor;
            }
            set
            {
                _minor = value;
            }
        }

        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <value></value>
        public int Build
        {
            get
            {
                return _build;
            }
            set
            {
                _build = value;
            }
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        /// <value></value>
        public int Revision
        {
            get
            {
                return _revision;
            }
            set
            {
                _revision = value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="SerializableVersion"/> instance.
        /// </summary>
        public SerializableVersion()
        {
            this._build = -1;
            this._revision = -1;
            this._major = 0;
            this._minor = 0;
        }

        /// <summary>
        /// Creates a new <see cref="SerializableVersion"/> instance.
        /// </summary>
        /// <param name="version">Version.</param>
        public SerializableVersion(string version)
        {
            this._build = -1;
            this._revision = -1;
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            char[] chArray1 = new char[1] { '.' };
            string[] textArray1 = version.Split(chArray1);
            int num1 = textArray1.Length;
            if ((num1 < 2) || (num1 > 4))
            {
                throw new ArgumentException("Arg_VersionString");
            }
            this._major = int.Parse(textArray1[0], CultureInfo.InvariantCulture);
            if (this._major < 0)
            {
                throw new ArgumentOutOfRangeException("version", "ArgumentOutOfRange_Version");
            }
            this._minor = int.Parse(textArray1[1], CultureInfo.InvariantCulture);
            if (this._minor < 0)
            {
                throw new ArgumentOutOfRangeException("version", "ArgumentOutOfRange_Version");
            }
            num1 -= 2;
            if (num1 > 0)
            {
                this._build = int.Parse(textArray1[2], CultureInfo.InvariantCulture);
                if (this._build < 0)
                {
                    throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
                }
                num1--;
                if (num1 > 0)
                {
                    this._revision = int.Parse(textArray1[3], CultureInfo.InvariantCulture);
                    if (this._revision < 0)
                    {
                        throw new ArgumentOutOfRangeException("revision", "ArgumentOutOfRange_Version");
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="SerializableVersion"/> instance.
        /// </summary>
        /// <param name="major">Major.</param>
        /// <param name="minor">Minor.</param>
        public SerializableVersion(int major, int minor)
        {
            this._build = -1;
            this._revision = -1;
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
            }
            this._major = major;
            this._minor = minor;
            this._major = major;
        }

        /// <summary>
        /// Creates a new <see cref="SerializableVersion"/> instance.
        /// </summary>
        /// <param name="major">Major.</param>
        /// <param name="minor">Minor.</param>
        /// <param name="build">Build.</param>
        public SerializableVersion(int major, int minor, int build)
        {
            this._build = -1;
            this._revision = -1;
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
            }
            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
            }
            this._major = major;
            this._minor = minor;
            this._build = build;
        }

        /// <summary>
        /// Creates a new <see cref="SerializableVersion"/> instance.
        /// </summary>
        /// <param name="major">Major.</param>
        /// <param name="minor">Minor.</param>
        /// <param name="build">Build.</param>
        /// <param name="revision">Revision.</param>
        public SerializableVersion(int major, int minor, int build, int revision)
        {
            this._build = -1;
            this._revision = -1;
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major", "ArgumentOutOfRange_Version");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor", "ArgumentOutOfRange_Version");
            }
            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build", "ArgumentOutOfRange_Version");
            }
            if (revision < 0)
            {
                throw new ArgumentOutOfRangeException("revision", "ArgumentOutOfRange_Version");
            }
            this._major = major;
            this._minor = minor;
            this._build = build;
            this._revision = revision;
        }

        #region ICloneable Members
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            SerializableVersion version1 = new SerializableVersion();
            version1._major = this._major;
            version1._minor = this._major;
            version1._build = this._build;
            version1._revision = this._revision;
            return version1;
        }
        #endregion

        #region IComparable Members

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">Obj.</param>
        /// <param name="version"></param>
        /// <returns></returns>
        public int CompareTo(object version)
        {
            if (version == null)
            {
                return 1;
            }

            if (!(version is Version))
            {
                throw new ArgumentException("Arg_MustBeVersion");
            }

            Version version1 = (Version)version;
            if (this._major != version1.Major)
            {
                if (this._major > version1.Major)
                {
                    return 1;
                }
                return -1;
            }

            if (this._minor != version1.Minor)
            {
                if (this._minor > version1.Minor)
                {
                    return 1;
                }
                return -1;
            }

            if (this._build != version1.Build)
            {
                if (this._build > version1.Build)
                {
                    return 1;
                }
                return -1;
            }

            if (this._revision == version1.Revision)
            {
                return 0;
            }

            if (this._revision > version1.Revision)
            {
                return 1;
            }

            return -1;
        }
        #endregion

        /// <summary>
        /// Equalss the specified obj.
        /// </summary>
        /// <param name="obj">Obj.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is SerializableVersion))
            {
                return false;
            }
            SerializableVersion version1 = (SerializableVersion)obj;
            if (((this._major == version1.Major) && (this._minor == version1.Minor)) && (this._build == version1.Build) && (this._revision == version1.Revision))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int num1 = 0;
            num1 |= ((this._major & 15) << 0x1c);
            num1 |= ((this._minor & 0xff) << 20);
            num1 |= ((this._build & 0xff) << 12);
            return (num1 | this._revision & 0xfff);
        }

        /// <summary>
        /// Operator ==s the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        /// <returns></returns>
        public static bool operator ==(SerializableVersion v1, SerializableVersion v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// Operator &gt;s the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        /// <returns></returns>
        public static bool operator >(SerializableVersion v1, SerializableVersion v2)
        {
            return (v2 < v1);
        }

        /// <summary>
        /// Operator &gt;=s the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        /// <returns></returns>
        public static bool operator >=(SerializableVersion v1, SerializableVersion v2)
        {
            return (v2 <= v1);
        }

        /// <summary>
        /// Operator !=s the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        /// <returns></returns>
        public static bool operator !=(SerializableVersion v1, SerializableVersion v2)
        {
            return (v1 != v2);
        }

        /// <summary>
        /// Operator &lt;s the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        /// <returns></returns>
        public static bool operator <(SerializableVersion v1, SerializableVersion v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException("v1");
            }
            return (v1.CompareTo(v2) < 0);
        }

        /// <summary>
        /// Operator &lt;=s the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        /// <param name="v2">V2.</param>
        /// <returns></returns>
        public static bool operator <=(SerializableVersion v1, SerializableVersion v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException("v1");
            }
            return (v1.CompareTo(v2) <= 0);
        }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this._build == -1)
            {
                return this.ToString(2);
            }
            if (this._revision == -1)
            {
                return this.ToString(3);
            }
            return this.ToString(4);
        }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="fieldCount">Field count.</param>
        /// <returns></returns>
        public string ToString(int fieldCount)
        {
            object[] objArray1;
            switch (fieldCount)
            {
                case 0:
                    {
                        return string.Empty;
                    }
                case 1:
                    {
                        return (this._major.ToString());
                    }
                case 2:
                    {
                        return (this._major.ToString() + "." + this._minor.ToString());
                    }
            }
            if (this._build == -1)
            {
                throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "2"), "fieldCount");
            }
            if (fieldCount == 3)
            {
                objArray1 = new object[5] { this._major, ".", this._minor, ".", this._build };
                return string.Concat(objArray1);
            }
            if (this._revision == -1)
            {
                throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "3"), "fieldCount");
            }
            if (fieldCount == 4)
            {
                objArray1 = new object[7] { this._major, ".", this._minor, ".", this._build, ".", this._revision };
                return string.Concat(objArray1);
            }
            throw new ArgumentException(string.Format("ArgumentOutOfRange_Bounds_Lower_Upper {0},{1}", "0", "4"), "fieldCount");
        }
    }
}
