using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Info;

namespace SimplyMobile.Device
{
    public class WindowsPhoneDevice : IDevice
    {
        private readonly IPhone phone;
        private readonly IBluetoothHub bluetoothHub;
        private readonly IScreen screen;

        public WindowsPhoneDevice()
        {
            this.phone = new WindowsPhone();
            this.bluetoothHub = new BluetoothHub();
            this.screen = new Screen();
        }

        #region IDevice Members

        public IPhone Phone
        {
            get { return this.phone; }
        }

        public IBluetoothHub BluetoothHub
        {
            get { return this.bluetoothHub; }
        }

        public IScreen Screen
        {
            get { return this.screen; }
        }

        public string Name 
        {
            get { return DeviceStatus.DeviceName; }
        }

        public string FirmwareVersion
        {
            get { return DeviceStatus.DeviceFirmwareVersion; }
        }

        public string HardwareVersion
        {
            get { return DeviceStatus.DeviceHardwareVersion; }
        }

        public string Manufacturer
        {
            get { return DeviceStatus.DeviceManufacturer; }
        }

        #endregion


    }
}
