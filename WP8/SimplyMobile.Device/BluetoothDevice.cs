using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace SimplyMobile.Device
{
    public class BluetoothDevice : IBluetoothDevice
    {
        private PeerInformation device;
        private StreamSocket socket;

        public BluetoothDevice(PeerInformation peerInfo)
        {
            this.device = peerInfo;
        }

        public string Name
        {
            get { return this.device.DisplayName; }
        }

        public string Address
        {
            get { return this.device.HostName.DisplayName; }
        }

        public Stream InputStream
        {
            get
            {
                if (this.socket == null)
                {
                    return null;
                }

                return this.socket.InputStream.AsStreamForRead();
            }
        }

        public Stream OutputStream
        {
            get 
            {
                if (this.socket == null)
                {
                    return null;
                }

                return this.socket.OutputStream.AsStreamForWrite();
            }
        }

        public async Task<DeviceConnectionResponse> Connect()
        {
            if (this.socket != null)
            {
                this.socket.Dispose();
            }

            try
            {
                this.socket = new StreamSocket();

                await this.socket.ConnectAsync(device.HostName, device.ServiceName);

                return DeviceConnectionResponse.Success;
            }
            catch (Exception ex)
            {
                this.socket.Dispose();
                this.socket = null;
                return DeviceConnectionResponse.Failure(new DeviceConnectionException(this, "StreamSocket.ConnectAsync", ex));
            }
        }

        public void Disconnect()
        {
            if (this.socket != null)
            {
                this.socket.Dispose();
                this.socket = null;
            }
        }
    }
}
