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
    /// <summary>
    /// Defines battery monitoring interface
    /// </summary>
    public interface IBattery
    {
        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level in percentage 0-100.
        /// </value>
        int Level { get; }

        /// <summary>
        /// Gets a value indicating whether battery is charging
        /// </summary>
        bool Charging { get; }

        /// <summary>
        /// Gets the current battery status.
        /// </summary>
        BatteryStatus Status { get; }

        /// <summary>
        ///  Occurs when level changes. 
        /// </summary>
        event EventHandler<EventArgs<int>> OnLevelChange;

        /// <summary>
        ///  Occurs when charger is connected or disconnected. 
        /// </summary>
        event EventHandler<EventArgs<bool>> OnChargerStatusChanged;
    }
}