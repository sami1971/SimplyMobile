using System;
using MonoTouch.UIKit;
using System.ComponentModel;
using MonoTouch.CoreTelephony;

namespace SimplyMobile.Device
{
    public class ApplePhone : AppleDevice
    {
        public enum PhoneType
        {
            [Description("Unknown device")]
            Unknown = 0,
            [Description("iPhone 1G")]
            iPhone1G = 1,
            [Description("iPhone 3G")]
            iPhone3G,
            [Description("iPhone 3GS")]
            iPhone3GS,
            [Description("iPhone 4 GSM")]
            iPhone4GSM,
            [Description("iPhone 4 CDMA")]
            iPhone4CDMA,
            [Description("iPhone 4S")]
            iPhone4S,
            [Description("iPhone 5 GSM")]
            iPhone5GSM,
            [Description("iPhone 5 CDMA")]
            iPhone5CDMA,
            [Description("iPhone 5C CDMA")]
            iPhone5C_CDMA,
            [Description("iPhone 5C GSM")]
            iPhone5C_GSM,
            [Description("iPhone 5S CDMA")]
            iPhone5S_CDMA,
            [Description("iPhone 5S GSM")]
            iPhone5S_GSM,
        }

        internal ApplePhone(int majorVersion, int minorVersion)
            : base()
        {
            this.Phone = new PhoneImplementation ();

            switch (majorVersion)
            {
            case 1:
                this.Version = minorVersion == 1 ? PhoneType.iPhone1G : PhoneType.iPhone3G;
                break;
            case 2:
                this.Version = PhoneType.iPhone3GS;
                break;
            case 3:
                this.Version = minorVersion == 1 ? PhoneType.iPhone4GSM : PhoneType.iPhone4CDMA;
                break;
            case 4:
                this.Version = PhoneType.iPhone4S;
                break;
            case 5:
                this.Version = PhoneType.iPhone5GSM + minorVersion - 1;
                break;
            case 6:
                this.Version = minorVersion == 1 ? PhoneType.iPhone5S_CDMA : PhoneType.iPhone5S_GSM;
                break;
            default:
                this.Version = PhoneType.Unknown;
                break;
            }

            if (majorVersion > 4)
            {
                this.Screen = new Screen (1136, 640, 326, 326);
            }
            else if (majorVersion > 2)
            {
                this.Screen = new Screen (960, 640, 326, 326);
            }
            else
            {
                this.Screen = new Screen (480, 320, 163, 163);
            }

            this.Name = this.HardwareVersion = this.Version.GetDescription ();
        }

        public PhoneType Version
        {
            get;
            private set;
        }
    }

    public class PhoneImplementation : IPhone
    {
        private static Lazy<CTTelephonyNetworkInfo> TelNet = new Lazy<CTTelephonyNetworkInfo> ();
        #region IPhone implementation

        public string CellularProvider
        {
            get
            {
                return TelNet.Value.SubscriberCellularProvider.CarrierName;
            }
        }

        public bool? IsCellularDataEnabled
        {
            get
            {
                return null;
            }
        }

        public bool? IsCellularDataRoamingEnabled
        {
            get
            {
                return null;
            }
        }

        public bool? IsNetworkAvailable
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the ISO Country Code
        /// </summary>
        public string ICC
        {
            get
            {
                return TelNet.Value.SubscriberCellularProvider.IsoCountryCode;
            }
        }

        /// <summary>
        /// Gets the Mobile Country Code
        /// </summary>
        public string MCC
        {
            get
            {
                return TelNet.Value.SubscriberCellularProvider.MobileCountryCode;
            }
        }

        /// <summary>
        /// Gets the Mobile Network Code
        /// </summary>
        public string MNC
        {
            get
            {
                return TelNet.Value.SubscriberCellularProvider.MobileNetworkCode;
            }
        }

        public void DialNumber(string number)
        {
            UIApplication.SharedApplication.OpenUrl (new MonoTouch.Foundation.NSUrl ("tel:" + number));
        }
        #endregion
    }
}

