//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using System;
using Microsoft.Phone.Info;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    /// <summary>
    /// The battery class.
    /// </summary>
    public static partial class Battery
    {
        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level in percentage 0-100.
        /// </value>
        public static int Level
        {
            get { return Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercent; }
        }

        /// <summary>
        /// Gets a value indicating whether battery is charging
        /// </summary>
        public static bool Charging
        {
            get
            {
                return DeviceStatus.PowerSource == PowerSource.External;
            }
        }

        #region partial implementations
        static partial void StartLevelMonitoring()
        {
            Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercentChanged += OnRemainingChargePercentChanged;
        }

        static partial void StopLevelMonitoring()
        {
            Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercentChanged -= OnRemainingChargePercentChanged;
        }

        static partial void StartChargerMonitoring()
        {
            DeviceStatus.PowerSourceChanged += OnPowerSourceChanged;
        }

        static partial void StopChargerMonitoring()
        {
            DeviceStatus.PowerSourceChanged -= OnPowerSourceChanged;
        }
        #endregion

        private static void OnRemainingChargePercentChanged(object sender, object o)
        {
            if (onLevelChange != null)
            {
                onLevelChange(sender, new EventArgs<int>(Level));
            }
        }

        private static void OnPowerSourceChanged(object sender, EventArgs eventArgs)
        {
            if (onChargerStatusChanged != null)
            {
                onChargerStatusChanged(sender, new EventArgs<bool>(Charging));
            }
        }
    }
}