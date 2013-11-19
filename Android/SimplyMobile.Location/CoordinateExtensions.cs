
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
				Accuracy = location.HasAccuracy ? location.Accuracy : null,
				Altitude = location.HasAltitude ? location.Altitude : null,
				Bearing = location.HasBearing ? location.Bearing : null,
				Speed = location.HasSpeed ? location.Speed : null,
			};
		}
	}
}

