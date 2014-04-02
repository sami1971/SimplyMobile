using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class AccelerometerImpl : IAccelerometer
    {
        public event EventHandler<Core.EventArgs<AccelometerStatus>> ReadingAvailable
        {
            add
            {
                Accelometer.ReadingAvailable += value;
            }
            remove
            {
                Accelometer.ReadingAvailable -= value;
            }
        }

        public AccelometerStatus LatestStatus
        {
            get { return Accelometer.LatestStatus; }
        }

        public AccelerometerInterval Interval
        {
            get
            {
                return Accelometer.Interval;
            }
            set
            {
                Accelometer.Interval = value;
            }
        }
    }
}
