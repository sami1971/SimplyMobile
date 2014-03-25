using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteBlobTests
{
    public class StoreBatteryData
    {
        private readonly IBattery battery;
        private readonly ICrudProvider db;

        public StoreBatteryData(IBattery battery, ICrudProvider db)
        {
            this.battery = battery;
            this.db = db;
        }

        public bool Start()
        {
            try
            {
                this.battery.OnLevelChange += battery_OnLevelChange;
                this.battery.OnChargerStatusChanged += battery_OnChargerStatusChanged;
                return true;
            }
            catch (Exception ex)
            {
                ex.TryToStore(this.db, this);
                return false;
            }
        }

        void battery_OnChargerStatusChanged(object sender, EventArgs<bool> e)
        {
            this.db.Create(this.battery.Status);
        }

        void battery_OnLevelChange(object sender, EventArgs<int> e)
        {
            this.db.Create(this.battery.Status);
        }

        public void Stop()
        {
            this.battery.OnLevelChange -= battery_OnLevelChange;
            this.battery.OnChargerStatusChanged -= battery_OnChargerStatusChanged;
        }
    }
}
