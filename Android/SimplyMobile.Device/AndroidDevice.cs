using System;
using Android.Content;
using Android.Telephony;
using Android.Bluetooth;

namespace SimplyMobile.Device
{
    public class AndroidDevice : IDevice
    {
        private static IDevice currentDevice;

        private AndroidDevice ()
        {
            var manager = this.GetSystemService(Context.TelephonyService) 
                as TelephonyManager;

            if (manager != null && manager.PhoneType != PhoneType.None)
            {
                this.Phone = new AndroidPhone();
            }

            if (BluetoothAdapter.DefaultAdapter != null)
            {
                this.BluetoothHub = new BluetoothHub(BluetoothAdapter.DefaultAdapter);
            }
        }

        public static IDevice CurrentDevice { get { return currentDevice ?? (currentDevice = new AndroidDevice()); } }

        #region IDevice implementation

        public IPhone Phone { get; private set; }

        public IBluetoothHub BluetoothHub { get; private set; }
        #endregion
    }
}

