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
using Android.App;
using Android.Content;
using Android.OS;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    /// <summary>
    /// Battery information class.
    /// </summary>
    public static partial class Battery
    {
        private static int? level;
        private static LevelMonitor levelMonitor;
        private static ChargerMonitor chargerMonitor;

        private static bool? chargerConnected;

        static partial void StartLevelMonitoring()
        {
            if (levelMonitor == null)
            {
                levelMonitor = new LevelMonitor();
            }
            levelMonitor.Start();
        }

        static partial void StopLevelMonitoring()
        {
            if (levelMonitor == null) return;
            levelMonitor.Stop();
            levelMonitor = null;
        }

        static partial void StartChargerMonitoring()
        {
            if (chargerMonitor == null)
            {
                chargerMonitor = new ChargerMonitor();
            }
            chargerMonitor.Start();
        }

        static partial void StopChargerMonitoring()
        {
            if (chargerMonitor == null) return;
            chargerMonitor.Stop();
            chargerMonitor = null;
        }

        /// <summary>
        /// Gets the level percentage from 0-100.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public static int Level
        {
            get { return GetLevel(); }
            private set
            {
                level = value;
                onLevelChange.Invoke (onLevelChange, level.Value);
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
                return GetChargerState();
            }
            private set
            {
                chargerConnected = value;
                onChargerStatusChanged.Invoke (onChargerStatusChanged, chargerConnected.Value);
            }
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <returns>The level.</returns>
        private static int GetLevel()
        {
            if (levelMonitor != null && level.HasValue)
            {
                return level.Value;
            }

            var f = -1;
            var intent = f.RegisterReceiver(null, new IntentFilter(Intent.ActionBatteryChanged));
            if (intent != null)
            {
                f = LevelMonitor.GetLevel(intent);
            }

            return f;
        }

        /// <summary>
        /// Gets the state of the charger.
        /// </summary>
        /// <returns><c>true</c>, if charger state was gotten, <c>false</c> otherwise.</returns>
        private static bool GetChargerState()
        {
            if (chargerMonitor != null && chargerConnected.HasValue)
            {
                return chargerConnected.Value;
            }

            var o = new object();

            var intent = o.RegisterReceiver(null, new IntentFilter(Intent.ActionBatteryChanged));
            if (intent == null)
            {
                return false;
            }

            int status = intent.GetIntExtra(Android.OS.BatteryManager.ExtraStatus, -1);
            return (status == (int)Android.OS.BatteryPlugged.Ac || status == (int)Android.OS.BatteryPlugged.Usb);
        }

        private class LevelMonitor : Monitor
        {
            public override void OnReceive(Context context, Intent intent)
            {
                Level = GetLevel(intent);
            }

            protected override IntentFilter Filter
            {
                get { return new IntentFilter(Intent.ActionBatteryChanged); }
            }

            public static int GetLevel(Intent intent)
            {
                var rawlevel = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
                var scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);

                if (rawlevel >= 0 && scale > 0)
                {
                    return rawlevel * 100 / scale;
                }

                return -1;
            }
        }

        private class ChargerMonitor : Monitor
        {
            protected override IntentFilter Filter
            {
                get
                {
                    var filter = new IntentFilter(Intent.ActionPowerConnected);
                    filter.AddAction(Intent.ActionPowerDisconnected);
                    return filter;
                }
            }

            public override void OnReceive(Context context, Intent intent)
            {
                Charging = intent.Action.Equals(Intent.ActionPowerConnected);
            }
        }
    }
}