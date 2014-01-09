using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimplyMobile.Location
{
    public static partial class LocationMonitor
    {
        public static async Task<Coordinates> GetCoordinatesAsync(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Start monitoring.
        /// </summary>
        static partial void StartMonitoring()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        static partial void StopMonitoring()
        {
            throw new NotImplementedException();
        }
    }
}