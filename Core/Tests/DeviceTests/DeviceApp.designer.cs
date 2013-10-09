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
        private static ObservableDataSource<BatteryStatus> batteryStatus;

        /// <summary>
        /// Gets the battery status.
        /// </summary>
        public static ObservableDataSource<BatteryStatus> BatteryStatus
        {
            get
            {
                return batteryStatus ?? (batteryStatus = new ObservableDataSource<BatteryStatus>());
            }
        }

        /// <summary>
        /// Call this when the application has finished loading.
        /// </summary>
        private void OnStart()
        {
            Battery.OnChargerStatusChanged += (s, a) => BatteryStatus.Data.Add(Battery.Status);
            Battery.OnLevelChange += (s, a) => BatteryStatus.Data.Add(Battery.Status);

        }

		private void OnStop()
		{

		}
    }
}