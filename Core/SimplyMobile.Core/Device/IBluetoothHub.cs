using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public interface IBluetoothHub
    {
        bool Enabled { get; }

        Task<IReadOnlyList<IBluetoothDevice>> GetPairedDevices();

        void OpenSettings();
    }
}
