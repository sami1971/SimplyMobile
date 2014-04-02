using System;
using SimplyMobile.Core;
using Android.Runtime;

namespace SmsBlockTest
{
    public class SmsBlockApp : MobileApp
    {
        public SmsBlockApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate ()
        {
            base.OnCreate ();

        }
    }
}

