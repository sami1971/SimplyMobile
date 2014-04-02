using System.Collections.Generic;

namespace SimplyMobile.Location.Bing
{
    /// <summary>
    /// The bing response.
    /// </summary>
    public class BingResponse
    {
        /// <summary>
        /// Gets or sets the authentication result code.
        /// </summary>
        public string AuthenticationResultCode { get; set; }

        /// <summary>
        /// Gets or sets the brand logo uri.
        /// </summary>
        public string BrandLogoUri { get; set; }

        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets the resource sets.
        /// </summary>
        public List<ResourceSet> ResourceSets { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the trace id.
        /// </summary>
        public string TraceId { get; set; }
    }

    /// <summary>
    /// The resource set.
    /// </summary>
    public class ResourceSet
    {
        /// <summary>
        /// Gets or sets the estimated total.
        /// </summary>
        public int EstimatedTotal { get; set; }

        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        public List<Resource> Resources { get; set; }
    }

    /// <summary>
    /// The resource.
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the bbox.
        /// </summary>
        public List<double> Bbox { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the confidence.
        /// </summary>
        public string Confidence { get; set; }

        /// <summary>
        /// Gets or sets the entity type.
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Gets or sets the geocode points.
        /// </summary>
        public List<GeocodePoint> GeocodePoints { get; set; }

        /// <summary>
        /// Gets or sets the match codes.
        /// </summary>
        public List<string> MatchCodes { get; set; }
    }

    /// <summary>
    /// The geocode point.
    /// </summary>
    public class GeocodePoint
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public List<double> Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the calculation method.
        /// </summary>
        public string CalculationMethod { get; set; }

        /// <summary>
        /// Gets or sets the usage types.
        /// </summary>
        public List<string> UsageTypes { get; set; }
    }

    /// <summary>
    /// The address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address line.
        /// </summary>
        public string AddressLine { get; set; }

        /// <summary>
        /// Gets or sets the admin district.
        /// </summary>
        public string AdminDistrict { get; set; }

        /// <summary>
        /// Gets or sets the admin district 2.
        /// </summary>
        public string AdminDistrict2 { get; set; }

        /// <summary>
        /// Gets or sets the country region.
        /// </summary>
        public string CountryRegion { get; set; }

        /// <summary>
        /// Gets or sets the formatted address.
        /// </summary>
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the locality.
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }
    }

    /// <summary>
    /// The point.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public List<double> Coordinates { get; set; }
    }
}

