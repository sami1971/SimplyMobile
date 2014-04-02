using System;
using MonoTouch.CoreBluetooth;

namespace SimplyMobile.Device
{
    public class BluetoothDevice : IBluetoothDevice
    {
        private readonly CBPeripheral device;

        public BluetoothDevice (CBPeripheral device)
        {
            this.device = device;
        }

        #region IBluetoothDevice implementation

        public System.Threading.Tasks.Task<DeviceConnectionResponse> Connect()
        {
            throw new NotImplementedException ();
        }

        public void Disconnect()
        {
            throw new NotImplementedException ();
        }

        public string Name
        {
            get
            {
                return device.Name;
            }
        }

        public string Address
        {
            get
            {
                return device.Identifier.AsString();
            }
        }

        public System.IO.Stream InputStream
        {
            get
            {
                throw new NotImplementedException ();
            }
        }

        public System.IO.Stream OutputStream
        {
            get
            {
                throw new NotImplementedException ();
            }
        }

        #endregion
    }
}

