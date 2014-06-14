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
        /// Gets the phone interface if device has it, otherwise null
        /// </summary>
        IPhone Phone { get; }

        /// <summary>
        /// Gets the bluetooth hub.
        /// </summary>
        /// <value>The bluetooth hub if available, otherwise null.</value>
        IBluetoothHub BluetoothHub { get; }

        /// <summary>
        /// Gets the screen information for the device.
        /// </summary>
        /// <value>The screen information.</value>
        IScreen Screen { get; }

        /// <summary>
        /// Gets the battery.
        /// </summary>
        /// <value>The battery.</value>
        IBattery Battery { get; }

        /// <summary>
        /// Gets the available sensors
        /// </summary>
//        IEnumerable<Type> Sensors { get; }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        string FirmwareVersion { get; }
        
        string HardwareVersion { get; }

        string Manufacturer { get; }

        bool SupportsSensor<T>() where T : ISensor;
    }
}
