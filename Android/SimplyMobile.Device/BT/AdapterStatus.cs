using System;

namespace SimplyMobile.Device.BT
{
    /// <summary>
    /// Bluetooth adapter status
    /// </summary>
    public enum AdapterStatus
    {
        /// <summary>
        /// Adapter is off.
        /// </summary>
        Off = 10,
        /// <summary>
        /// Adapter is turning on
        /// </summary>
        TurningOn,
        /// <summary>
        /// Adapter is on
        /// </summary>
        On,
        /// <summary>
        /// Adapter is turning off
        /// </summary>
        TurningOff
    }
}

