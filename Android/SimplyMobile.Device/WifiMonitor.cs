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
using Android.Net.Wifi;
using SimplyMobile.Core;
using Android.Util;
using System.Linq;

namespace SimplyMobile.Device
{
    public enum  WifiApState
    {
        WIFI_AP_STATE_UNKNOWN = -1,
        WIFI_AP_STATE_DISABLING = 0,
        WIFI_AP_STATE_DISABLED = 1,
        WIFI_AP_STATE_ENABLING = 2,
        WIFI_AP_STATE_ENABLED = 3,
        WIFI_AP_STATE_FAILED = 4
    }

    public partial class WifiMonitor : Monitor
    {
//        private WifiManager wifiManager;
        private const string SetWifiApConfigurationMethod = "setWifiApConfiguration";
        private const string GetWifiApConfigurationMethod = "getWifiApConfiguration";

        private const string Intent = "android.net.wifi.WIFI_STATE_CHANGED";
        private static WifiManager wifiManager;

        public static WifiManager WifiManager
        {
            get
            {
                return wifiManager ?? 
                    (wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager);
            }
        }

        public bool Enabled
        {
            get
            {
//                var wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
                return WifiManager != null && WifiManager.IsWifiEnabled;
            }
        }

        /// <summary>
        /// Gets the current SSID.
        /// </summary>
        /// <value>The current SSID.</value>
        public string CurrentSSID 
        { 
            get
            {
                if (WifiManager == null)
                {
                    return string.Empty;
                }

                return WifiManager.ConnectionInfo.SSID;
            }
        }

        public bool TrySetState(bool enabled)
        {
            if (this.Enabled == enabled)
            {
                return true;
            }

            try
            {

//              var wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
                return WifiManager != null && WifiManager.SetWifiEnabled(enabled);
            }
            catch (Exception exception)
            {
                this.ReportException(exception);
                return false;
            }
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(Intent))
            {
                this.OnStatusChange.Invoke(this, this.Enabled);
            }
        }

        #region AP Configuration
        /// <summary>
        /// Sets the WiFi AP state.
        /// </summary>
        /// <returns><c>true</c>, if WiFi state was set, <c>false</c> otherwise.</returns>
        /// <param name="wifiConfig">Wifi config.</param>
        /// <param name="enabled">State enabled.</param>
        public bool SetWifiApEnabled(WifiConfiguration wifiConfig, Java.Lang.Boolean enabled) 
        {
            try
            {
                if (enabled == Java.Lang.Boolean.True)
                { // disable WiFi in any case
                    WifiManager.SetWifiEnabled(false);
                }

                var method = WifiManager.Class.GetMethod("setWifiApEnabled", wifiConfig.Class, enabled.Class);
                return (Boolean) method.Invoke(WifiManager, wifiConfig, enabled);
            } 
            catch (Exception e) 
            {
                Log.Error(this.ToString(), e.Message);
                return false;
            }
        }

        /// <summary>
        /// Sets the WiFi AP configuration.
        /// </summary>
        /// <returns><c>true</c>, if wifi ap configuration was set, <c>false</c> otherwise.</returns>
        /// <param name="wifiConfig">Wifi config.</param>
        public bool SetWifiApConfiguration(WifiConfiguration wifiConfig)
        {
            try
            {
                foreach (var m in WifiManager.Class.GetMethods())
                {
                    Log.Info(m.Name, m.Accessible.ToString());
                }

                var method = WifiManager.Class.GetMethods().FirstOrDefault(a => a.Name == SetWifiApConfigurationMethod);

//              var b = new Java.Lang.Boolean(true);
//              var method = WifiManager.Class.GetMethod (SetWifiApConfigurationMethod, b.Class);

                return method != null && (bool)method.Invoke (WifiManager, wifiConfig);
            }
            catch (Exception ex)
            {
                Log.Error (this.ToString (), ex.Message);
                return false;
            }
        }

        public WifiConfiguration GetWifiApConfiguration() 
        {
            try 
            {
                var method = WifiManager.Class.GetMethod(GetWifiApConfigurationMethod);
                return method.Invoke(WifiManager) as WifiConfiguration;
            }
            catch (Exception ex) 
            {
                Log.Error(this.ToString(), ex.Message);
                return null;
            }
        }

        public WifiApState WifiApState 
        {
            get
            {
                try
                {
                    var method = WifiManager.Class.GetMethod ("getWifiApState");

                    var ret = method.Invoke (WifiManager);
                    var tmp = (int)ret;

                    // Fix for Android 4
                    if (tmp > 10)
                    {
                        tmp = tmp - 10;
                    }

                    return (WifiApState)tmp;
                } 
                catch (Exception ex)
                {
                    Log.Error (this.ToString (), ex.Message);
                    return WifiApState.WIFI_AP_STATE_FAILED;
                }
            }
        }
        #endregion

        protected override IntentFilter Filter
        {
            get { return new IntentFilter(Intent); }
        }
    }
}