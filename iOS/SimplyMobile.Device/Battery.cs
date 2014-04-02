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
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public static partial class Battery
    {
        /// <summary>
        /// Starts the level monitor.
        /// </summary>
        static partial void StartLevelMonitoring()
        {
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
            NSNotificationCenter.DefaultCenter.AddObserver
            (
                UIDevice.BatteryLevelDidChangeNotification,
                (NSNotification n) =>
                {
                    if (onLevelChange != null)
                    {
                        onLevelChange(onLevelChange, new EventArgs<int>(Level));
                    }
                }
            );
        }

        static partial void StopLevelMonitoring()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver
            (
                UIDevice.BatteryLevelDidChangeNotification
            );

            // if charger monitor does not have subscribers then lets disable battery monitoring
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = (onChargerStatusChanged != null);
        }

        static partial void StopChargerMonitoring()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver
            (
                UIDevice.BatteryStateDidChangeNotification
            );

            // if level monitor does not have subscribers then lets disable battery monitoring
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = (onLevelChange != null);
        }

        static partial void StartChargerMonitoring()
        {
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
            NSNotificationCenter.DefaultCenter.AddObserver
            (
                UIDevice.BatteryStateDidChangeNotification,
                (NSNotification n) =>
                {
                    if (onChargerStatusChanged != null)
                    {
                        onChargerStatusChanged(onChargerStatusChanged, new EventArgs<bool>(Charging));
                    }
                }
            );
        }

        /// <summary>
        ///  Gets the battery level. 
        /// </summary>
        /// <returns>Battery level in percentage, 0-100</returns>
        public static int Level
        {
            get
            {
                UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
                return (int)(UIDevice.CurrentDevice.BatteryLevel * 100);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SimplyMobile.Device.Battery"/> is charging.
        /// </summary>
        /// <value><c>true</c> if charging; otherwise, <c>false</c>.</value>
        public static bool Charging
        {
            get 
            { 
                return UIDevice.CurrentDevice.BatteryState != UIDeviceBatteryState.Unplugged; 
            }
        }

    }
}