using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimplyMobile.Device
{
    public interface IBluetoothDevice
    {
        string Name { get; }
        string Address { get; }

        Stream InputStream { get; }
        Stream OutputStream { get; }

        Task<DeviceConnectionResponse> Connect();
        void Disconnect();
    }
}
