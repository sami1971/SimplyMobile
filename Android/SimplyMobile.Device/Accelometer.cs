using System;
using Android.Hardware;
using SimplyMobile.Core;
using Android.Content;
using Android.App;
using Android.Util;

namespace SimplyMobile.Device
{
    public static partial class Accelometer
    {
        private static AccelometerMonitor monitor;

        private static AccelometerMonitor Monitor
        {
            get
            {
                return monitor ?? (monitor = new Accelometer.AccelometerMonitor ());
            }
        }

        public static SensorDelay Delay { get; private set; }

        public static AccelerometerInterval Interval
        {
            get
            {
                switch (Delay) 
                {
                case SensorDelay.Fastest:
                    return AccelerometerInterval.Fastest;
                case SensorDelay.Game:
                    return AccelerometerInterval.Game;
                case SensorDelay.Normal:
                    return AccelerometerInterval.Normal;
                default:
                    return AccelerometerInterval.Ui;
                }
            }
            set
            {
                Accelometer.Delay = SensorDelay.Ui;
            }
        }

        static partial void StartMonitoring()
        {
            Monitor.Start();
        }

        static partial void StopMonitoring()
        {
            Monitor.Stop();
        }

        private class AccelometerMonitor  : Java.Lang.Object, ISensorEventListener, IMonitor
        {
            private SensorManager sensorManager;
            private Sensor accelerometer;

            #region ISensorEventListener implementation

            public void OnAccuracyChanged (Sensor sensor, SensorStatus accuracy)
            {
//              throw new NotImplementedException ();
            }

            public void OnSensorChanged (SensorEvent e)
            {
                if (e.Sensor.Type != SensorType.Accelerometer)
                {
                    return;
                }

                var reading = new AccelometerStatus(e.Values[0], e.Values[1], e.Values[2]);

                Accelometer.LatestStatus = new AccelometerStatus (reading);

                Accelometer.readingAvailable.Invoke (this, reading);
            }

            #endregion

            #region IMonitor implementation

            public event EventHandler<EventArgs<bool>> OnActiveChanged;

            public event EventHandler<EventArgs<Exception>> OnException;

            public bool Start ()
            {
                if (this.Active)
                {
                    return this.Active;
                }

                this.sensorManager = Application.Context.GetSystemService(Context.SensorService) as SensorManager;
                if (this.sensorManager == null)
                {
                    this.OnException.Invoke (this, new Exception ("Unable to get sensor manager."));
                    return false;
                }

                this.accelerometer = sensorManager.GetDefaultSensor(SensorType.Accelerometer);

                Log.Info(this.accelerometer.ToString(), this.accelerometer.MaximumRange.ToString());

                if (this.accelerometer == null)
                {
                    this.sensorManager = null;
                    this.OnException.Invoke (this, new Exception ("Unable to get Accelerometer."));
                    return false;
                }

                this.sensorManager.RegisterListener(this, accelerometer, Accelometer.Delay);

                return this.Active;
            }

            public void Stop ()
            {
                if (this.Active)
                {
                    this.sensorManager.UnregisterListener(this);
                    this.sensorManager = null;
                    this.accelerometer = null;
                }
            }

            public bool Active
            {
                get
                {
                    return (this.sensorManager != null && this.accelerometer != null);
                }
            }

            #endregion


        }
    }


}

