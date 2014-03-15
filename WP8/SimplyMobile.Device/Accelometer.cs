using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Devices.Sensors;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public static partial class Accelometer
    {
        private static Accelerometer accelerometer;

        public static AccelerometerInterval Interval { get; set; }

        static partial void StartMonitoring()
        {
            accelerometer = new Accelerometer { TimeBetweenUpdates = TimeSpan.FromMilliseconds((long)Interval) };

            accelerometer.CurrentValueChanged += AccelerometerOnCurrentValueChanged;
            accelerometer.Start();
        }

        static partial void StopMonitoring()
        {
            if (accelerometer != null)
            {
                accelerometer.CurrentValueChanged -= AccelerometerOnCurrentValueChanged;
                accelerometer.Stop();
                accelerometer = null;
            }
        }

        private static void AccelerometerOnCurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> sensorReadingEventArgs)
        {
            if (readingAvailable != null)
            {
                var ac = sensorReadingEventArgs.SensorReading.Acceleration;
                readingAvailable.Invoke(
                    accelerometer, 
                    new AccelometerStatus(ac.X, ac.Y, ac.Z));
            }
        }
    }
}
