
namespace SimplyMobile.Location
{
    using Android.Locations;

    public static class CoordinateExtensions
    {
        public static Coordinates GetCoordinates(this Location location)
        {
            return new Coordinates()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Accuracy = location.HasAccuracy ? (double?)location.Accuracy : null,
                Altitude = location.HasAltitude ? (double?)location.Altitude : null,
                Bearing = location.HasBearing ? (double?)location.Bearing : null,
                Speed = location.HasSpeed ? (double?)location.Speed : null,
            };
        }
    }
}

