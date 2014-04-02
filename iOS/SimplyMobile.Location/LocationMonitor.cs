using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;

namespace SimplyMobile.Location
{
    public static partial class LocationMonitor
    {
        /// <summary>
        /// Gets or sets the desired accuracy.
        /// </summary>
        public static Accuracy DesiredAccuracy
        {
            get
            {
                return locationManager.DesiredAccuracy <= 10
                           ? Accuracy.High
                           : Accuracy.NoRequirement;
            }

            set
            {
                locationManager.DesiredAccuracy = (value == Accuracy.High) ?
                        5 :
                        25;
            }
        }

        /// <summary>
        /// Gets or sets the location change threshold.
        /// </summary>
        public static double LocationChangeThreshold
        {
            get { return LocationManager.DistanceFilter; }
            set { LocationManager.DistanceFilter = value; }
        }

        public static uint Interval { get; set; }

        public static bool IsEnabled
        {
            get
            {
                return CLLocationManager.LocationServicesEnabled;
            }
        }

        public static async Task<Coordinates> GetCoordinatesAsync(TimeSpan age, TimeSpan timeout)
        {
            return await Task.Factory.StartNew(() => LocationManager.Location.GetCoordinates());
        }

        /// <summary>
        /// The location manager.
        /// </summary>
        private static CLLocationManager locationManager;

        /// <summary>
        /// Gets the location manager.
        /// </summary>
        private static CLLocationManager LocationManager { get { return locationManager ?? (locationManager = new CLLocationManager()); } }

        /// <summary>
        /// The location manager on locations updated.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="clLocationsUpdatedEventArgs">
        /// The locations updated event args.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private static void LocationManagerOnLocationsUpdated(object sender, CLLocationsUpdatedEventArgs clLocationsUpdatedEventArgs)
        {
            if (locationChanged == null)
            {
                StopMonitoring();
                return;
            }

            foreach (var location in clLocationsUpdatedEventArgs.Locations)
            {
                locationChanged(sender, location.GetCoordinates());
            }
        }

        /// <summary>
        /// Start monitoring.
        /// </summary>
        static partial void StartMonitoring()
        {
            LocationManager.LocationsUpdated += LocationManagerOnLocationsUpdated;            
            //LocationManager.Failed += HandleFailed;
            //LocationManager.LocationUpdatesPaused += HandleLocationUpdatesPaused;
            LocationManager.StartUpdatingLocation();
        }

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        static partial void StopMonitoring()
        {
            LocationManager.LocationsUpdated -= LocationManagerOnLocationsUpdated;
            LocationManager.StopUpdatingLocation();
            // clear the manager
            locationManager.Dispose();
            locationManager = null;
        }
    }
}