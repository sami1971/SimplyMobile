using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class DeviceOrientation : IOrientationMonitor
    {
        public static void RegisterMonitor()
        {
        }

        #region IOrientationMonitor implementation

        public event EventHandler<SimplyMobile.Core.EventArgs<Orientation>> OnOrientationChange;

        public Orientation CurrentOrientation
        {
            get
            {
                throw new NotImplementedException ();
            }
        }

        #endregion
    }
}
