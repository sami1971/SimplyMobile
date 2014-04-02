using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    using Core;
    /// <summary>
    /// Dummy battery implementation for testing purposes
    /// </summary>
    public class DummyBattery : IBattery
    {
        private int level;
        private bool charging;
        
        public int Level
        {
            get { return level; }
            set
            {
                if (this.level != value)
                {
                    this.level = value;
                    if (this.OnLevelChange != null)
                    {
                        this.OnLevelChange.Invoke<int>(this, this.level);
                    }
                }
            }
        }

        public bool Charging
        {
            get { return this.charging; }
            set
            {
                if (this.charging != value)
                {
                    this.charging = value;
                    if (this.OnChargerStatusChanged != null)
                    {
                        this.OnChargerStatusChanged.Invoke<bool>(this, this.charging);
                    }
                }
            }
        }

        public BatteryStatus Status
        {
            get { return this.GetStatus(); }
        }

        public event EventHandler<EventArgs<int>> OnLevelChange;

        public event EventHandler<EventArgs<bool>> OnChargerStatusChanged;
    }
}
