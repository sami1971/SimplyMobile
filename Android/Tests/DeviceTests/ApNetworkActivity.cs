using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Device;
using Android.Util;

namespace DeviceTests
{
    [Activity (Label = "ApNetworkActivity")]            
    public class ApNetworkActivity : ActivityCore
    {
//      private ObservableDataSource<string> networks = new ObservableDataSource<string> ();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.ap_networks);

            var wifiMonitor = new WifiMonitor ();

//          Log.Info (wifiMonitor.ToString (), wifiMonitor.WifiApState.ToString());
//
//          wifiMonitor.SetWifiApConfiguration (new Android.Net.Wifi.WifiConfiguration ());

            var wifiConfig = wifiMonitor.GetWifiApConfiguration ();
        }
    }
}

