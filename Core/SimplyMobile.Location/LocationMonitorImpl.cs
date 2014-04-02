using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Location
{
    /// <summary>
    /// Wrapper around the static LocationMonitor class, 
    /// implements ILocationMonitor to be used in Portable Class Libraries
    /// </summary>
    public class LocationMonitorImpl : ILocationMonitor
    {

        public LocationMonitorImpl(TimeSpan interval)
        {
            LocationMonitor.Interval = (uint)interval.TotalMilliseconds;
        }

        public LocationMonitorImpl(Accuracy accuracy)
        {
            DesiredAccuracy = accuracy;
        }

        public event EventHandler<Coordinates> LocationChanged
        {
            add { LocationMonitor.LocationChanged += value; }
            remove { LocationMonitor.LocationChanged -= value; }
        }

        public bool IsEnabled
        {
            get
            {
                return LocationMonitor.IsEnabled;
            }
        }

        public Accuracy DesiredAccuracy
        {
            get
            {
                return LocationMonitor.DesiredAccuracy;
            }
            set
            {
                LocationMonitor.DesiredAccuracy = value;
            }
        }

        public double LocationChangeThreshold
        {
            get
            {
                return LocationMonitor.LocationChangeThreshold;
            }
            set
            {
                LocationMonitor.LocationChangeThreshold = value;
            }
        }

        public async Task<Coordinates> GetCoordinatesAsync(TimeSpan age, TimeSpan timeout)
        {
            return await LocationMonitor.GetCoordinatesAsync(age, timeout);
        }
    }
}
