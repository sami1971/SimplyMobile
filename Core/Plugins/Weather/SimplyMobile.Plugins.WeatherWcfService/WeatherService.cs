using System.Threading;
using System.Threading.Tasks;
using System;

namespace SimplyMobile.Plugins.WeatherWcfService
{
    public class WeatherService : IWeatherService
    {
        private IWeatherForecastService service;

        public WeatherService()
        {
            this.service = WeatherForecastServiceClient.Default;
        }

        public async Task<string[]> GetCitiesByCountryAsync(string country, IProgress<Exception> progress = null)
        {
            string[] ret = new string[0];

            await Task.Factory.StartNew(() =>
            {
                AutoResetEvent e = new AutoResetEvent(false);
                service.BeginGetCitiesByCountry(
                    country,
                    (r) =>
                    {
                        try
                        {
                            ret = service.EndGetCitiesByCountry(r);
                        }
                        catch (Exception ex)
                        {
                            if (progress != null)
                            {
                                progress.Report(ex);
                            }
                        }
                        finally
                        {
                            e.Set();
                        }
                    },
                    null);
                e.WaitOne();
            });

            return ret;
        }

        public async Task<Weather> GetWeatherAsync(string city, string country, IProgress<Exception> progress = null)
        {
            Weather ret = null;

            await Task.Factory.StartNew(() =>
            {
                AutoResetEvent e = new AutoResetEvent(false);

                service.BeginGetForecastByCity(
                    city,
                    country,
                    (r) =>
                    {
                    try
                    {
                        ret = service.EndGetForecastByCity(r);
                    }
                    catch(Exception ex)
                    {
                        if (progress != null)
                        {
                            progress.Report(ex);
                        }
                    }
                    finally
                    {
                        e.Set();
                    }
                    },
                null);
                e.WaitOne();
            });

//          await task.ContinueWith(
//                tsk => { return ret; },
//                TaskContinuationOptions.OnlyOnFaulted);

//            await task;

            return ret;
        }
    }
}
