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

namespace SimplyMobile.Device
{
    public partial class WifiMonitor : Monitor
    {
//        private WifiManager wifiManager;

        private const string Intent = "android.net.wifi.WIFI_STATE_CHANGED";

        public bool Enabled
        {
            get
            {
                var wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
                return wifiManager != null && wifiManager.IsWifiEnabled;
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
				var wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
				return wifiManager != null && wifiManager.SetWifiEnabled(enabled);
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

        protected override IntentFilter Filter
        {
            get { return new IntentFilter(Intent); }
        }
    }
}