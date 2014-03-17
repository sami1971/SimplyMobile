using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Devices.Sensors;

namespace SimplyMobile.Device
{
    public partial class MotionSensor : IMotionSensor
    {
        private Motion sensor;

        public MotionSensor(TimeSpan updateInterval)
        {
            if (this.IsSupported)
            {
                this.sensor = new Motion()
                {
                    TimeBetweenUpdates = updateInterval
                };
            }
        }

        public bool IsSupported
        {
            get { return Motion.IsSupported; }
        }

        partial void Start()
        {
            if (this.sensor == null)
            {
                throw new NotSupportedException("Motion sensor not supported by this device");
            }

            this.sensor.CurrentValueChanged += sensor_CurrentValueChanged;
        }

        partial void Stop()
        {
            if (this.sensor != null)
            {
                this.sensor.CurrentValueChanged -= sensor_CurrentValueChanged;
            }
        }

        void sensor_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            
        }
    }
}
