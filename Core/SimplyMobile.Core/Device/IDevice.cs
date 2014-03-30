using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    /// <summary>
    /// Device interface
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Phone interface, if available, otherwise should be null
        /// </summary>
        IPhone Phone { get; }

        IBluetoothHub BluetoothHub { get; }
    }
}
