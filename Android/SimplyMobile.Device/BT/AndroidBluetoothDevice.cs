using System;
using Android.Bluetooth;
using System.IO;
using Android.Util;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class AndroidBluetoothDevice : IBluetoothDevice
    {
        private const string BtUuid = "00001101-0000-1000-8000-00805F9B34FB";

        private readonly BluetoothDevice device;
        private BluetoothSocket socket;

        public AndroidBluetoothDevice (BluetoothDevice device)
        {
            this.device = device;
        }

        #region IBluetoothDevice implementation

        public async Task<DeviceConnectionResponse> Connect()
        {
            if (this.socket == null)
            {
                Java.Util.UUID uuid = Java.Util.UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
                this.socket = this.device.CreateRfcommSocketToServiceRecord(uuid);
            }

            try
            {
                await this.socket.ConnectAsync();
                return DeviceConnectionResponse.Success;
            }
            catch (Java.IO.IOException ex)
            {
                Log.Error ("BluetoothSocket.Connect()", ex.Message);
                return DeviceConnectionResponse.Failure(
                    new DeviceConnectionException(this, "BluetoothSocket.ConnectAsync()", ex));
            }
        }

        public void Disconnect()
        {
            if (this.socket != null)
            {
                try
                {
                    this.socket.Close();
                    this.socket = null;
                }
                catch (Java.IO.IOException ex)
                {
                    Log.Error("BluetoothSocket.Close()", ex.Message);
                }
            }
        }

        public string Name
        {
            get { return this.device.Name; }
        }

        public string Address
        {
            get { return this.device.Address; }
        }

        public Stream InputStream 
        { 
            get 
            { 
                return (this.socket == null) ? null : this.socket.InputStream; 
            } 
        }

        public Stream OutputStream 
        { 
            get 
            { 
                return (this.socket == null) ? null : this.socket.OutputStream; 
            } 
        }

        #endregion
    }
}

