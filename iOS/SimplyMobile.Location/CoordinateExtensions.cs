using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SimplyMobile.Location
{
    public static class CoordinateExtensions
    {
        public static Coordinates GetCoordinates(this CLLocation location)
        {
            return new Coordinates()
                {
                    Accuracy = location.HorizontalAccuracy,
                    Altitude = location.Altitude,
                    Speed = location.Speed,
                    Latitude = location.Coordinate.Latitude,
                    Longitude = location.Coordinate.Longitude
                };
        }
    }
}