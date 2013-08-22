using System;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public static partial class Battery
    {
        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public static float Level
        {
            get;
            private set;
        }
    }
}