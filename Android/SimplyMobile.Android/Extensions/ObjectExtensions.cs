using Android.App;
using Android.Content;
using Android.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplyMobile
{
    public static class ObjectExtensions
    {
        public static Java.Lang.Object GetSystemService(this object o, string service)
        {
            return Application.Context.GetSystemService(service);
        }

        public static ConnectivityManager GetConnectivityManager(this object o)
        {
            return (ConnectivityManager)o.GetSystemService(Context.ConnectivityService);
        }
    }
}
