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
using System.Diagnostics.CodeAnalysis;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public static partial class Battery
    {


        /// <summary>
        /// Event handler for battery level changes. 
        /// </summary>
        public static event EventHandler<EventArgs<int>> OnLevelChange
        {
            add
            {
                if (onLevelChange == null)
                {
                    StartLevelMonitoring();
                }
                onLevelChange += value;
            }
            remove 
            { 
                onLevelChange -= value;
                if (onLevelChange == null)
                {
                    StopLevelMonitoring();
                }
            }
        }

        /// <summary>
        ///  Event handler for charger connection changes. 
        /// </summary>
        public static event EventHandler<EventArgs<bool>> OnChargerStatusChanged
        {
            add
            {
                if (onChargerStatusChanged == null)
                {
                    StartChargerMonitoring();
                }
                onChargerStatusChanged += value;
            }
            remove
            {
                onChargerStatusChanged -= value;
                if (onChargerStatusChanged == null)
                {
                    StopChargerMonitoring();
                }
            }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public static BatteryStatus Status
        {
            get
            {
                return new BatteryStatus()
                    {
                        Time = new DateTimeOffset(DateTime.UtcNow),
                        ChargerConnected = Battery.Charging,
                        Level = Battery.Level
                    };
            }
        }

        /// <summary>
        /// Private event handler for battery level changes. 
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
        private static event EventHandler<EventArgs<int>> onLevelChange;

        /// <summary>
        /// Private event handler for charger connection changes. 
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
        private static event EventHandler<EventArgs<bool>> onChargerStatusChanged;

        /// <summary>
        /// Start level monitoring
        /// </summary>
        /// <remarks>Partial method to be implemented by platform specific provider.</remarks>
        static partial void StartLevelMonitoring();

        /// <summary>
        /// Stop level monitoring
        /// </summary>
        /// <remarks>Partial method to be implemented by platform specific provider.</remarks>
        static partial void StopLevelMonitoring();

        /// <summary>
        /// Start charger monitoring
        /// </summary>
        /// <remarks>Partial method to be implemented by platform specific provider.</remarks>
        static partial void StartChargerMonitoring();

        /// <summary>
        /// Stop charger monitoring
        /// </summary>
        /// <remarks>Partial method to be implemented by platform specific provider.</remarks>
        static partial void StopChargerMonitoring();
    }
}