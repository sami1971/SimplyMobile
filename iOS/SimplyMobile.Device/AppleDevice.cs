using System;
using System.Text.RegularExpressions;
using SimplyMobile.iOS;
using MonoTouch.UIKit;

namespace SimplyMobile.Device
{
    public abstract class AppleDevice : IDevice
    {
        private const string iPhoneExpression = "iPhone([1-6]),([1-4])";
        private const string iPodExpression = "iPod([1-5]),([1])";
        private const string iPadExpression = "iPad([1-4]),([1-6])";

        private static IDevice device;

        protected AppleDevice()
        {
            this.BluetoothHub = new BluetoothHub ();
            this.Battery = new BatteryImpl ();
            this.FirmwareVersion = UIDevice.CurrentDevice.SystemVersion;
        }

        public static IDevice CurrentDevice ()
        {
            if (device != null)
            {
                return device;
            }

            var hardwareVersion = SysCtrl.GetSystemProperty ("hw.machine");

            var regex = new Regex (iPhoneExpression).Match(hardwareVersion);
            if (regex.Success)
            {
                return device = new ApplePhone (int.Parse (regex.Groups [1].Value), int.Parse (regex.Groups [2].Value));
            }

            regex = new Regex (iPodExpression).Match (hardwareVersion);
            if (regex.Success)
            {
                return device = new Pod (int.Parse (regex.Groups [1].Value), int.Parse (regex.Groups [2].Value));
            }

            regex = new Regex (iPadExpression).Match (hardwareVersion);
            if (regex.Success)
            {
                return device = new Pad (int.Parse (regex.Groups [1].Value), int.Parse (regex.Groups [2].Value));
            }

            return device = new Simulator ();

//

//

//
//            if ([platform isEqualToString:@"i386"])         return @"Simulator";
//            if ([platform isEqualToString:@"x86_64"])       return @"Simulator";
        }

        #region IDevice implementation

        public IPhone Phone
        {
            get;
            protected set;
        }

        public IBluetoothHub BluetoothHub
        {
            get;
            protected set;
        }

        public bool SupportsSensor<T>() where T : ISensor
        {
            throw new NotImplementedException ();
        }

        public IScreen Screen
        {
            get;
            protected set;
        }

        public IBattery Battery
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public string FirmwareVersion
        {
            get;
            protected set;
        }

        public string HardwareVersion
        {
            get;
            protected set;
        }

        public string Manufacturer
        {
            get
            {
                return "Apple";
            }
        }

        #endregion
    }
}

