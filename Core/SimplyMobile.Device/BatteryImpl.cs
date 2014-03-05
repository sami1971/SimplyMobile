using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class BatteryImpl : IBattery
    {
        public int Level
        {
            get { return Battery.Level; }
        }

        public bool Charging
        {
            get { return Battery.Charging; }
        }

        /// <summary>
        /// Gets the current battery status.
        /// </summary>
        public BatteryStatus Status 
        {
            get { return Battery.Status; } 
        }

        public event EventHandler<Core.EventArgs<int>> OnLevelChange
        {
            add
            {
                Battery.OnLevelChange += value;
            }
            remove
            {
                Battery.OnLevelChange -= value;
            }
        }

        public event EventHandler<Core.EventArgs<bool>> OnChargerStatusChanged
        {
            add
            {
                Battery.OnChargerStatusChanged += value;
            }
            remove
            {
                Battery.OnChargerStatusChanged -= value;
            }
        }
    }
}
