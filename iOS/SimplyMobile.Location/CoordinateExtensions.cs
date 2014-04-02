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
                Longitude = location.Coordinate.Longitude,
                TimeStamp = location.Timestamp
                };
        }

        public static CLLocationCoordinate2D Get2DLocation(this Coordinates coordinates)
        {
            return new CLLocationCoordinate2D (coordinates.Latitude, coordinates.Longitude);
        }

        public static CLLocation GetLocation(this Coordinates coordinates)
        {
            return new CLLocation (
                coordinates.Get2DLocation (), 
                coordinates.Altitude.HasValue ? coordinates.Altitude.Value : 0, 
                coordinates.Accuracy.HasValue ? coordinates.Accuracy.Value : 0, 
                0, 
                coordinates.TimeStamp);
        }
    }
}