using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimplyMobile.Plugins.WeatherWcfService
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            MainAsync(args).Wait();                             
        }

        static async Task MainAsync(string[] args)
        {
            var eHandler = new ExceptionHandler ();

            IWeatherService service = new WeatherService();

            var w = await service.GetWeatherAsync("TAMPA", "USA", eHandler);

            var country = "USA";
            var cities = await service.GetCitiesByCountryAsync(country, eHandler);

            foreach (var city in cities)
            {
                var weather = await service.GetWeatherAsync(city, country, eHandler);
                if (weather != null)
                {
                    Console.WriteLine(weather.Time);
                }
            }
        }

        private class ExceptionHandler : IProgress<Exception>
        {
            #region IProgress implementation
            public void Report (Exception value)
            {
                var e = value;

                while (e != null)
                {
                    Console.WriteLine (e.Message);
                    e = e.InnerException;
                }
            }
            #endregion
        }
    }
}
