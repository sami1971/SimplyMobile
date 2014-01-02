using System;
using System.Collections.Generic;

namespace SimplyMobile.Location.Bing
{

	public class BingResponse
	{
		public string AuthenticationResultCode { get; set; }
		public string BrandLogoUri { get; set; }
		public string Copyright { get; set; }
		public List<ResourceSet> ResourceSets { get; set; }
		public int StatusCode { get; set; }
		public string StatusDescription { get; set; }
		public string TraceId { get; set; }
	}

	public class ResourceSet
	{
		public int EstimatedTotal { get; set; }
		public List<Resource> Resources { get; set; }
	}

	public class Resource
	{
		public string Type { get; set; }
		public List<double> Bbox { get; set; }
		public string Name { get; set; }
		public Point Point { get; set; }
		public Address Address { get; set; }
		public string Confidence { get; set; }
		public string EntityType { get; set; }
		public List<GeocodePoint> GeocodePoints { get; set; }
		public List<string> MatchCodes { get; set; }
	}

	public class GeocodePoint
	{
		public string Type { get; set; }
		public List<double> Coordinates { get; set; }
		public string CalculationMethod { get; set; }
		public List<string> UsageTypes { get; set; }
	}

	public class Address
	{
		public string AddressLine { get; set; }
		public string AdminDistrict { get; set; }
		public string AdminDistrict2 { get; set; }
		public string CountryRegion { get; set; }
		public string FormattedAddress { get; set; }
		public string Locality { get; set; }
		public string PostalCode { get; set; }
	}

	public class Point
	{
		public string Type { get; set; }
		public List<double> Coordinates { get; set; }
	}
}

