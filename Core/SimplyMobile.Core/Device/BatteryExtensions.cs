using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public static class BatteryExtensions
    {
        public static BatteryStatus GetStatus(this IBattery battery)
        {
            return new BatteryStatus()
            {
                Time = new DateTimeOffset(DateTime.UtcNow),
                ChargerConnected = battery.Charging,
                Level = battery.Level
            };
        }
    }
}
