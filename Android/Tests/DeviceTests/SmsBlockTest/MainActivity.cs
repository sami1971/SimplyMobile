using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SmsBlockTest
{
    using SimplyMobile.Messaging;


    [Activity (Label = "SmsBlockTest", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private SmsBlocker blocker;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

        }

        protected override void OnPause ()
        {
            base.OnPause ();
            blocker.Stop ();
            blocker = null;
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            blocker = new SmsBlocker ();
            blocker.Start ();
        }
    }
}


