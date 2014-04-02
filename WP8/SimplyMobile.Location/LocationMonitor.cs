using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SimplyMobile.Location
{
    public static partial class LocationMonitor
    {
        /// <summary>
        /// The geo-locator.
        /// </summary>
        private static Geolocator geolocator;

        /// <summary>
        /// Gets or sets the desired accuracy.
        /// </summary>
        public static Accuracy DesiredAccuracy
        {
            get
            {
                return (Geolocator.DesiredAccuracy == PositionAccuracy.Default)
                           ? Accuracy.NoRequirement
                           : Accuracy.High;
            }

            set
            {
                Geolocator.DesiredAccuracy = (value == Accuracy.NoRequirement) ?
                        PositionAccuracy.Default :
                        PositionAccuracy.High;
            }
        }

        /// <summary>
        /// Gets or sets the location change threshold.
        /// </summary>
        public static double LocationChangeThreshold
        {
            get { return Geolocator.MovementThreshold; }
            set { Geolocator.MovementThreshold = value; }
        }

        public static uint Interval
        {
            get { return Geolocator.ReportInterval; }
            set { Geolocator.ReportInterval = value; }
        }

        /// <summary>
        /// Gets the geo-locator.
        /// </summary>
        private static Geolocator Geolocator
        {
            get
            {
                return geolocator ?? (geolocator = new Geolocator());
            }
        }

        public static bool IsEnabled
        {
            get
            {
                var status = Geolocator.LocationStatus;
                return status != PositionStatus.Disabled &&
                    status != PositionStatus.NotAvailable;
            }
        }

        /// <summary>
        /// The get coordinates async.
        /// </summary>
        /// <param name="age">
        /// The age.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<Coordinates> GetCoordinatesAsync(TimeSpan age, TimeSpan timeout)
        {
            var position = await Geolocator.GetGeopositionAsync(age, timeout);
            return position.Coordinate.GetCoordinates();
        }

        /// <summary>
        /// Start monitoring.
        /// </summary>
        static partial void StartMonitoring()
        {
            Geolocator.PositionChanged += GeolocatorOnPositionChanged;
        }

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        static partial void StopMonitoring()
        {
            Geolocator.PositionChanged -= GeolocatorOnPositionChanged;
        }

        /// <summary>
        /// The position changed handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void GeolocatorOnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (locationChanged != null)
            {
                locationChanged(sender, args.Position.Coordinate.GetCoordinates());
            }
            else
            {
                StopMonitoring();
            }
        }
    }
}
