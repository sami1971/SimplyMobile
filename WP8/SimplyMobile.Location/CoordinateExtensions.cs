using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SimplyMobile.Location
{
    public static class CoordinateExtensions
    {
        public static Coordinates GetCoordinates(this Geocoordinate geocoordinate)
        {
            return new Coordinates()
                {
                    Accuracy = geocoordinate.Accuracy,
                    Altitude = geocoordinate.Altitude,
                    Bearing = geocoordinate.Heading,
                    Latitude = geocoordinate.Latitude,
                    Longitude = geocoordinate.Longitude,
                    Speed = geocoordinate.Speed
                };
        }
    }
}
