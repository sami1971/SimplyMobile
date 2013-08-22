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
    public static partial class Battery
    {
        private static float level;
        private static LevelMonitor levelMonitor;
        private static ChargerMonitor chargerMonitor;

        private static bool chargerConnected;

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
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public static float Level
        {
            get { return GetLevel(); }
            private set
            {
                level = value;
                if (onLevelChange != null)
                {
                    onLevelChange(onLevelChange, new EventArgs<float>(level));
                }
            }
        }

        public static bool Charging
        {
            get
            {
                return GetChargerState();
            }
            private set
            {
                chargerConnected = value;
                if (onChargerStatusChanged != null)
                {
                    onChargerStatusChanged(onChargerStatusChanged, new EventArgs<bool>(chargerConnected));
                }
            }
        }

        private static float GetLevel()
        {
            if (levelMonitor != null && levelMonitor.Active)
            {
                return level;
            }

            float f = -1;
            var intent = f.RegisterReceiver(null, new IntentFilter(Intent.ActionBatteryChanged));
            if (intent != null)
            {
                f = LevelMonitor.GetLevel(intent);
            }

            return f;
        }

        private static bool GetChargerState()
        {
            if (chargerMonitor != null && chargerMonitor.Active)
            {
                return chargerConnected;
            }

            var o = new object();

            var intent = o.RegisterReceiver(null, new IntentFilter(Intent.ActionPowerConnected));
            return intent != null;
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

            public static float GetLevel(Intent intent)
            {
                var rawlevel = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
                var scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);
                float lvl = -1;
                if (rawlevel >= 0 && scale > 0)
                {
					lvl = (float)rawlevel / (float)scale;
                }
                return lvl;
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