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
using MonoTouch.SystemConfiguration;
using SimplyMobile.Core;
using MonoTouch.Foundation;

namespace SimplyMobile.Device
{
    public partial class WifiMonitor
    {
        #region IWifiMonitor Members

        public event EventHandler<EventArgs<bool>> OnActiveChanged;

        public event EventHandler<EventArgs<Exception>> OnException;

        public bool Active { get; private set; }

        public bool Enabled
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the current SSID.
        /// </summary>
        /// <value>The current SSID.</value>
        public string CurrentSSID 
        {
            get
            {
                NSDictionary dict;
                var status = CaptiveNetwork.TryCopyCurrentNetworkInfo ("en0", out dict);
                if (status == StatusCode.NoKey)
                {
                    return string.Empty;
                }

                var bssid = dict [CaptiveNetwork.NetworkInfoKeyBSSID];
                var ssid = dict [CaptiveNetwork.NetworkInfoKeySSID];
                var ssiddata = dict [CaptiveNetwork.NetworkInfoKeySSIDData];

                return ssid.ToString();
            }
        }

        public bool TrySetState(bool enabled)
        {
            return false;
        }

        #endregion

        #region IMonitor Members


        public bool Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        #endregion 

    }
}