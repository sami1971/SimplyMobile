using System;

namespace SimplyMobile.Device
{
    /// <summary>
    /// The battery status
    /// </summary>
    public class BatteryStatus
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether charger connected.
        /// </summary>
        public bool ChargerConnected { get; set; }

        /// <summary>
        /// The to string override.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}\tLevel\t{1}\tCharger\t{2}", this.Time, this.Level, this.ChargerConnected);
        }
    }
}
