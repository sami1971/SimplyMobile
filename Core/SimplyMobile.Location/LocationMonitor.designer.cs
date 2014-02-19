using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SimplyMobile.Core;

namespace SimplyMobile.Location
{
    /// <summary>
    /// The location monitor.
    /// </summary>
    public static partial class LocationMonitor
    {
        /// <summary>
        /// The location changed event.
        /// </summary>
        public static event EventHandler<Coordinates> LocationChanged
        {
            add
            {
                if (locationChanged == null)
                {
                    StartMonitoring();
                }

                locationChanged += value;
            }

            remove
            {
                locationChanged -= value;
                if (locationChanged == null)
                {
                    StopMonitoring();
                }
            }
        }

        /// <summary>
        /// The location changed private event.
        /// </summary>
        private static event EventHandler<Coordinates> locationChanged;

        /// <summary>
        /// Start monitoring.
        /// </summary>
        static partial void StartMonitoring();

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        static partial void StopMonitoring();
    }
}
