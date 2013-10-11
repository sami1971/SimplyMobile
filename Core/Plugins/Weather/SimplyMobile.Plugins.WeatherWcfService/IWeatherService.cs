using System.Threading.Tasks;

namespace SimplyMobile.Plugins.WeatherWcfService
{
    /// <summary>
    /// Simplified Weather Service Interface
    /// </summary>
    public interface IWeatherService
    {
        Task<string[]> GetCitiesByCountryAsync(string country);
        Task<Weather> GetWeatherAsync(string city, string country);
    }
}
