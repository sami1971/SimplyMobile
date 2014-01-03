using System;
using System.Threading.Tasks;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web;

namespace SimplyMobile.Location.Bing
{
    /// <summary>
    /// The Bing client.
    /// </summary>
    public class BingClient
	{
        /// <summary>
        /// The restful service client.
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        /// The application key for Bing mapping services.
        /// </summary>
        private string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingClient"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public BingClient (string key)
		{
			this.key = key;
			this.restClient = new RestClient(
				new Uri("http://dev.virtualearth.net/REST/v1/Locations/"), 
				new JsonSerializer());
		}

        /// <summary>
        /// The get method.
        /// </summary>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ServiceResponse<BingResponse>> Get(double latitude, double longitude)
		{
			return await this.restClient.GetAsync<BingResponse>(
				string.Format("{0},{1}?o=json&key={2}",  latitude, longitude, this.key),
				Format.Json);
		}

        /// <summary>
        /// The get method.
        /// </summary>
        /// <param name="coordinates">
        /// The coordinates.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ServiceResponse<BingResponse>> Get(Coordinates coordinates)
		{
			return await this.Get(coordinates.Latitude, coordinates.Longitude);
		}
	}
}

