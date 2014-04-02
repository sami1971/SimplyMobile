using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Core;

namespace BindingTests
{
    [Activity (Label = "BindingTests", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ExtensionViewModel model;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            model = new ExtensionViewModel ();
        }

        protected override void OnPause ()
        {
            base.OnPause ();
            model.Unbind ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            var button = FindViewById<Button> (Resource.Id.myButton);
            var label = FindViewById<TextView> (Resource.Id.label);
            var textView = FindViewById<EditText> (Resource.Id.textView);

            var toggle1 = FindViewById<ToggleButton> (Resource.Id.toggleButton1);
            var toggle2 = FindViewById<ToggleButton> (Resource.Id.toggleButton2);

            var switch1 = FindViewById<Switch> (Resource.Id.switch1);

            var seekBar = FindViewById<SeekBar> (Resource.Id.seekBar1);

            button.OnClick (model.OnButtonClick);
            button.BindTitle (model, "ButtonTitle");
            label.Bind(model, "TextField");
            textView.Bind(model, "TextField");

            toggle1.Bind (model, "ToggleOn");
            toggle2.Bind (model, "ToggleOn");
            switch1.Bind (model, "ToggleOn");

            seekBar.Bind (model, "SliderValue");
        }


    }
}


