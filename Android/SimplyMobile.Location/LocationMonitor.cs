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
using Android.Locations;

namespace SimplyMobile.Location
{
    public static partial class LocationMonitor
    {
        private static LocationListener listener;

        /// <summary>
        /// Gets or sets the desired accuracy.
        /// </summary>
        public static Accuracy DesiredAccuracy { get; set; }

        public static async Task<Coordinates> GetCoordinatesAsync(TimeSpan age, TimeSpan timeout)
        {
            return await Task.Factory.StartNew (() =>
            {
                var locationManager = (LocationManager)Application.Context.GetSystemService (Context.LocationService);
                var criteria = new Criteria {
                    Accuracy = DesiredAccuracy == Accuracy.High ? Android.Locations.Accuracy.Fine : Android.Locations.Accuracy.Coarse
                };

                var provider = locationManager.GetBestProvider (criteria, true);

                return locationManager.GetLastKnownLocation (provider).GetCoordinates();
            });
        }

        /// <summary>
        /// Gets or sets the location change threshold.
        /// </summary>
        public static double LocationChangeThreshold { get; set; }

        public static uint Interval { get; set; }

        public static bool IsEnabled
        {
            get
            {
                try
                {
                    var locationManager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);
                    return locationManager.AllProviders.Any();
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Start monitoring.
        /// </summary>
        static partial void StartMonitoring()
        {
            listener = new LocationListener();

            var locationManager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);

            if (locationManager == null)
            {
                return;
            }

            var criteria = new Criteria
            {
                Accuracy = DesiredAccuracy == Accuracy.High ? Android.Locations.Accuracy.Fine : Android.Locations.Accuracy.Coarse
            };

            var provider = locationManager.GetBestProvider(criteria, true);

            locationManager.RequestLocationUpdates(provider, 0, (float)LocationChangeThreshold, listener);
        }

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        static partial void StopMonitoring()
        {
            var locationManager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);

            if (locationManager != null)
            {
                locationManager.RemoveUpdates(listener);
            }

            listener.Dispose();
            listener = null;
        }

        private class LocationListener : Java.Lang.Object, ILocationListener
        {
            #region ILocationListener implementation

            public void OnLocationChanged(Android.Locations.Location location)
            {
                if (locationChanged != null)
                {
                    locationChanged (this, location.GetCoordinates ());
                }
            }

            public void OnProviderDisabled(string provider)
            {
                throw new NotImplementedException ();
            }

            public void OnProviderEnabled(string provider)
            {
                throw new NotImplementedException ();
            }

            public void OnStatusChanged(string provider, Availability status, Bundle extras)
            {
                throw new NotImplementedException ();
            }
            #endregion
        }
    }
}