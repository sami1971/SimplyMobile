using System.Threading;
using System.Threading.Tasks;

namespace SimplyMobile.Plugins.WeatherWcfService
{
    public class WeatherService : IWeatherService
    {
        private IWeatherForecastService service;

        public WeatherService()
        {
            this.service = WeatherForecastServiceClient.Default;
        }

        public async Task<string[]> GetCitiesByCountryAsync(string country)
        {
			string[] ret = null;

			await Task.Factory.StartNew (() =>
			{

				AutoResetEvent e = new AutoResetEvent(false);
				service.BeginGetCitiesByCountry (
					country, 
					(r) =>
					{
						ret = service.EndGetCitiesByCountry (r);
						e.Set();
					}, 
					null);
				e.WaitOne();
			});

			return ret;
        }

        public async Task<Weather> GetWeatherAsync(string city, string country)
        {
			Weather ret = null;

			await Task.Factory.StartNew (() =>
			{
				AutoResetEvent e = new AutoResetEvent(false);
				service.BeginGetForecastByCity (
					city,
					country, 
					(r) =>
					{
						ret = service.EndGetForecastByCity (r);
						e.Set();
					}, 
				null);
				e.WaitOne();
			});

			return ret;
        }
    }
}
