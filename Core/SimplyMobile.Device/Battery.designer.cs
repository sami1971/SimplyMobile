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
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public static partial class Battery
    {
        private static event EventHandler<EventArgs<int>> onLevelChange;
        private static event EventHandler<EventArgs<bool>> onChargerStatusChanged;

        static partial void StartLevelMonitoring();
        static partial void StopLevelMonitoring();

        static partial void StartChargerMonitoring();
        static partial void StopChargerMonitoring();

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


    }
}