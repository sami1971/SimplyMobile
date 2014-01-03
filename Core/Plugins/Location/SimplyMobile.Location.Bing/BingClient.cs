using System;
using SimplyMobile.Web;
using System.Threading.Tasks;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Text;

namespace SimplyMobile.Location.Bing
{
	public class BingClient
	{
		private IRestClient restClient;
		private string key;

		public BingClient (string key)
		{
			this.key = key;
			this.restClient = new RestClient(
				new Uri("http://dev.virtualearth.net/REST/v1/Locations/"), 
				new JsonSerializer());
		}

		public async Task<ServiceResponse<BingResponse>> Get(double latitude, double longitude)
		{
			return await restClient.GetAsync<BingResponse>(
				string.Format("{0},{1}?o=json&key={2}",  latitude, longitude, this.key),
				Format.Json);
		}

		public async Task<ServiceResponse<BingResponse>> Get(Coordinates coordinates)
		{
			return await Get(coordinates.Latitude, coordinates.Longitude);
		}
	}
}

