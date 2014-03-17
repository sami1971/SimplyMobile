using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Devices.Sensors;

namespace SimplyMobile.Device
{
    using Core;

    public partial class Gyroscope
    {
        private Microsoft.Devices.Sensors.Gyroscope gyroscope;

        public Gyroscope(TimeSpan updateInterval)
        {
            if (this.IsSupported)
            {
                this.gyroscope = new Microsoft.Devices.Sensors.Gyroscope()
                {
                    TimeBetweenUpdates = updateInterval
                };

                this.gyroscope.CurrentValueChanged += gyroscope_CurrentValueChanged;
            }
        }

        public bool IsSupported
        {
            get
            {
                return Microsoft.Devices.Sensors.Gyroscope.IsSupported;
            }
        }

        void gyroscope_CurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            var reading = e.SensorReading;
            var rate = reading.RotationRate;
            this.readingAvailable.Invoke<GyroReading>(
                sender, 
                new GyroReading(reading.Timestamp, rate.X, rate.Y, rate.Z));
        }

        partial void Start()
        {
            if (this.gyroscope == null)
            {
                throw new NotSupportedException("Gyroscope not supported on this device");
            }
            this.gyroscope.Start();
        }

        partial void Stop()
        {
            if (this.gyroscope != null)
            {
                this.gyroscope.Stop();
            }
        }
    }
}
