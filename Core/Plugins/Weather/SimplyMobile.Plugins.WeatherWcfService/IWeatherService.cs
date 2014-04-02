using System.Threading.Tasks;
using System;

namespace SimplyMobile.Plugins.WeatherWcfService
{
    /// <summary>
    /// Simplified Weather Service Interface
    /// </summary>
    public interface IWeatherService
    {
        Task<string[]> GetCitiesByCountryAsync(string country, IProgress<Exception> progress);
        Task<Weather> GetWeatherAsync(string city, string country, IProgress<Exception> progress);
    }
}
