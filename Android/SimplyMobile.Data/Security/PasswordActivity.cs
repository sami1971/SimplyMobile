using System;
using Android.Preferences;
using Android.App;

namespace SimplyMobile.Data
{
    [Activity(Label="Password")]
    public class PasswordActivity : PreferenceActivity
    {
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        }
    }
}

