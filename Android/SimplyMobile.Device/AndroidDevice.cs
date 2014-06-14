using System;
using Android.Content;
using Android.Telephony;
using Android.Bluetooth;
using Android.OS;

namespace SimplyMobile.Device
{
    public class AndroidDevice : IDevice
    {
        public bool SupportsSensor<T>() where T : ISensor
        {
            throw new NotImplementedException ();
        }

        private static IDevice currentDevice;

        private AndroidDevice ()
        {
            var manager = Context.TelephonyService.GetSystemService() as TelephonyManager;

            if (manager != null && manager.PhoneType != PhoneType.None)
            {
                this.Phone = new AndroidPhone();
            }

            if (BluetoothAdapter.DefaultAdapter != null)
            {
                this.BluetoothHub = new BluetoothHub(BluetoothAdapter.DefaultAdapter);
            }

            this.Screen = new Screen ();

            this.Manufacturer = Build.Manufacturer;
            this.Name = Build.Model;
            this.HardwareVersion = Build.Hardware;
            this.FirmwareVersion = Build.VERSION.Release;

            this.Battery = new BatteryImpl ();
        }

        public static IDevice CurrentDevice { get { return currentDevice ?? (currentDevice = new AndroidDevice()); } }

        #region IDevice implementation

        public IPhone Phone { get; private set; }

        public IScreen Screen
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string FirmwareVersion
        {
            get;
            private set;
        }

        public string HardwareVersion
        {
            get;
            private set;
        }

        public string Manufacturer
        {
            get;
            private set;
        }

        public IBluetoothHub BluetoothHub { get; private set; }

        public IBattery Battery { get; private set; }
        #endregion
    }
}

