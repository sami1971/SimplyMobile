using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public interface IAccelerometer
    {
        event EventHandler<EventArgs<AccelometerStatus>> ReadingAvailable;

        AccelometerStatus LatestStatus { get; }

        AccelerometerInterval Interval { get; set; }
    }
}
