using Android.App;
using Android.Content;
using Android.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SimplyMobile
{
    public static class ObjectExtensions
    {
        public static Java.Lang.Object GetSystemService(this object o, string service)
        {
            return Application.Context.GetSystemService(service);
        }

        public static Java.Lang.Object GetSystemService(this string service)
        {
            return Application.Context.GetSystemService(service);
        }

        public static ConnectivityManager GetConnectivityManager(this object o)
        {
            return (ConnectivityManager)o.GetSystemService(Context.ConnectivityService);
        }

        public static JavaObject<T> ToJavaObject<T>(this T o)
        {
            return new JavaObject<T> (o);
        }

        public static void StartActivity(this object o, Intent intent)
        {
            var context = o as Context;
            if (context != null)
            {
                context.StartActivity (intent);
            } 
            else
            {
                intent.SetFlags (ActivityFlags.NewTask);
                Application.Context.StartActivity (intent);
            }
        }

//        public static void StartActivityForResult2(this object o, Intent intent)
//        {
//            var context = o as Activity;
//            if (context != null)
//            {
//                context.StartActivityForResult (intent);
//            } 
//            else
//            {
//                intent.SetFlags (ActivityFlags.NewTask);
//                Application.Context.StartActivityForResult (intent);
//            }
//        }
    }
}
