using System;
using System.Runtime.Serialization;

namespace SimplyMobile.Location
{
    /// <summary>
    /// The coordinates class.
    /// </summary>
    [DataContract]
    public class Coordinates
    {
        /// <summary>
        /// The equator radius.
        /// </summary>
        public const double EquatorRadius = 6376500.0;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [DataMember]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [DataMember]
        public double Longitude { get; set; }

        #region optional items
        [DataMember]
        public double? Accuracy { get; set; }
        [DataMember]
        public double? Altitude { get; set; }
        [DataMember]
        public double? Bearing { get; set; }
        [DataMember]
        public double? Speed { get; set; }
        #endregion

        public DateTime TimeStamp { get; set; }

        public Coordinates()
        {
        }

        public Coordinates(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Calculates distance between two locations.
        /// </summary>
        /// <returns>The <see cref="System.Double"/>The distance in meters</returns>
        /// <param name="a">Location a</param>
        /// <param name="b">Location b</param>
        public static double DistanceBetween(Coordinates a, Coordinates b)
        {
            var d1 = a.Latitude * (Math.PI / 180.0);
            var num1 = a.Longitude * (Math.PI / 180.0);
            var d2 = b.Latitude * (Math.PI / 180.0);
            var num2 = b.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return EquatorRadius * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        /// <summary>
        /// Calculates bearing between start and stop.
        /// </summary>
        /// <returns>The <see cref="System.Double"/>.</returns>
        /// <param name="start">Start coordinates.</param>
        /// <param name="stop">Stop coordinates.</param>
        public static double BearingBetween(Coordinates start, Coordinates stop)
        {
            var deltaLon = stop.Longitude - start.Longitude;
            var cosStop = Math.Cos(stop.Latitude);
            return Math.Atan2(
                (Math.Cos(start.Latitude) * Math.Sin(stop.Latitude)) -
                (Math.Sin(start.Latitude) * cosStop * Math.Cos(deltaLon)),
                Math.Sin(deltaLon) * cosStop);
        }

        /// <summary>
        /// Calculates this locations distance to another coordicate.
        /// </summary>
        /// <returns>The distance to another coordicate</returns>
        /// <param name="other">Other coordinates.</param>
        public double DistanceFrom(Coordinates other)
        {
            return DistanceBetween(this, other);
        }

        /// <summary>
        /// Calculates this locations bearing to another coordicate.
        /// </summary>
        /// <returns>Bearing degree.</returns>
        /// <param name="other">Other coordinates.</param>
        public double BearingFrom(Coordinates other)
        {
            return BearingBetween(this, other);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("({0:0.0000}, {1:0.0000})", Latitude, Longitude);
        }
    }
}

