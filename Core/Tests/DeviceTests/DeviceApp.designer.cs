using System;
using System.Collections.ObjectModel;
using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Device;

namespace DeviceTests
{
    public partial class DeviceApp : MobileApp
    {
        /// <summary>
        /// The battery status.
        /// </summary>
        private static ObservableDataSource batteryStatus;

        /// <summary>
        /// Gets the battery status.
        /// </summary>
        public static ObservableDataSource BatteryStatus
        {
            get
            {
                return batteryStatus ?? (batteryStatus = new ObservableDataSource());
            }
        }

        /// <summary>
        /// Call this when the application has finished loading.
        /// </summary>
        private void OnLoadFinished()
        {
            Battery.OnChargerStatusChanged += (s, a) => BatteryStatus.Data.Add(Battery.Status);
            Battery.OnLevelChange += (s, a) => BatteryStatus.Data.Add(Battery.Status);
        }
    }
}