using System;
using System.ComponentModel;

namespace SimplyMobile.Device
{
    public class Pod : AppleDevice
    {
        public enum PodVersion
        {
            [Description("iPod Touch 1G")]
            FirstGeneration = 1,
            [Description("iPod Touch 2G")]
            SecondGeneration,
            [Description("iPod Touch 3G")]
            ThirdGeneration,
            [Description("iPod Touch 4G")]
            FourthGeneration,
            [Description("iPod Touch 5G")]
            FifthGeneration
        }

        internal Pod (int majorVersion, int minorVersion)
            : base()
        {
            this.Version = (PodVersion)majorVersion;
            this.Phone = null;

            this.Name = this.HardwareVersion = this.Version.GetDescription ();

            if (majorVersion > 4)
            {
                this.Screen = new Screen (1136, 640, 326, 326);
            }
            else if (majorVersion > 3)
            {
                this.Screen = new Screen (960, 640, 326, 326);
            }
            else
            {
                this.Screen = new Screen (480, 320, 163, 163);
            }
        }

        public PodVersion Version
        {
            get;
            private set;
        }

        #region IDevice implementation
        #endregion
    }
}

