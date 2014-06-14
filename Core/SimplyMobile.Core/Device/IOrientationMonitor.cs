using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public interface IOrientationMonitor
    {
        Orientation CurrentOrientation { get; }
        event EventHandler<EventArgs<Orientation>> OnOrientationChange;
    }
}
