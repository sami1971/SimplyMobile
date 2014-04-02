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

using SimplyMobile.Device;
using System.Diagnostics;
using Android.Util;

namespace DeviceTests
{
    [Activity (Label = "SensorDelayActivity")]          
    public class SensorDelayActivity : Activity
    {
        Stopwatch watch = new Stopwatch ();
        List<long> readings;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

            SetContentView (Resource.Layout.sensor_delay);
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            FindViewById<RadioGroup> (Resource.Id.radioGroup1).CheckedChange += HandleCheckedChange;
        }

        protected override void OnPause ()
        {
            base.OnPause ();
            FindViewById<RadioGroup> (Resource.Id.radioGroup1).CheckedChange -= HandleCheckedChange;
        }

        void HandleCheckedChange (object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            readings = new List<long> ();
            watch.Reset ();

            Accelometer.ReadingAvailable -= HandleReadingAvailable;
            switch (e.CheckedId) 
            {
            case Resource.Id.radioButton1:
                Accelometer.Interval = AccelerometerInterval.Fastest;
                break;
            case Resource.Id.radioButton2:
                Accelometer.Interval = AccelerometerInterval.Game;
                break;
            case Resource.Id.radioButton3:
                Accelometer.Interval = AccelerometerInterval.Normal;
                break;
            case Resource.Id.radioButton4:
                Accelometer.Interval = AccelerometerInterval.Ui;
                break;
            }
            Accelometer.ReadingAvailable += HandleReadingAvailable;
        }

        void HandleReadingAvailable (object sender, SimplyMobile.Core.EventArgs<AccelometerStatus> e)
        {
            if (watch.IsRunning) 
            {
                this.readings.Add (watch.ElapsedMilliseconds);
                if (this.readings.Count > 100) 
                {
                    var info = string.Format ("Average for 100 items was {0}ms.", this.readings.Average ());
                    Log.Info (Accelometer.Delay.ToString(), info);
                    this.readings.Clear ();
                    FindViewById<TextView>(Resource.Id.textView1).Text = info;
                }
            } 
            watch.Restart();
        }
    }
}

