using System;
using System.Threading;

namespace SimplyMobile.Plugins.WeatherWcfService
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IWeatherForecastService service = WeatherForecastServiceClient.Default;

            AutoResetEvent e = new AutoResetEvent(false);

			service.BeginGetCitiesByCountry (
                "FINLAND", 
                (r) =>
				{
					var cities = service.EndGetCitiesByCountry (r);
                    foreach (var city in cities)
                    {
                        Console.WriteLine(city);
                    }
                    e.Set();
				}, 
				null);

            e.WaitOne();								
		}
	
	}
}
