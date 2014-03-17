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
using Android.Hardware;

namespace SimplyMobile.Device
{
    using Core;

    public partial class Gyroscope
    {
        private GyroscopeListener listener;

        private SensorDelay rate;

        public Gyroscope(SensorDelay rate)
        {
            this.rate = rate;
        }

        public bool IsSupported
        {
            get
            {
                var manager = Application.Context.GetSystemService(Context.SensorService) as SensorManager;
                return (manager != null  && manager.GetDefaultSensor(SensorType.Gyroscope) != null);
            }
        }

        partial void Start()
        {
            this.Try(() =>
                {
                    this.listener = new GyroscopeListener(e =>
                        this.readingAvailable.Invoke<GyroReading>(
                            this, 
                            new GyroReading(e.Values[0], e.Values[1], e.Values[2])));

                    var manager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
                    manager.RegisterListener(this.listener, manager.GetDefaultSensor(SensorType.Gyroscope), this.rate);
                });
        }

        partial void Stop()
        {
            this.Try(() =>
                {
                    var manager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
                    manager.UnregisterListener(this.listener);
                    this.listener = null;
                });
        }

        private class GyroscopeListener : Java.Lang.Object, ISensorEventListener
        {
            public delegate void OnSensor(SensorEvent e);

            private OnSensor onSensor;

            public GyroscopeListener(OnSensor onSensorChanged)
            {
                this.onSensor = onSensorChanged;
            }

            public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
            {
                //throw new NotImplementedException();
            }

            public void OnSensorChanged(SensorEvent e)
            {
                if (e.Sensor.Type != SensorType.Gyroscope)
                {
                    return;
                }

                this.onSensor(e);
            }
        }
    }
}