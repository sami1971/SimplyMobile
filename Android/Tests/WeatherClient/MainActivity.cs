using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Plugins.WeatherWcfService;
using System.Threading;

namespace WeatherClient
{
    [Activity (Label = "WeatherClient", MainLauncher = true)]
    public class MainActivity : Activity, IProgress<Exception>
    {

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.myButton);

            button.Click += HandleClick;
//          button.Click += async delegate {
//
//
//              var weatherService = new WeatherService();
//              var country = "FINLAND";
//              var cities = await weatherService.GetCitiesByCountryAsync(country);
//              foreach(var city in cities)
//              {
//                  var weather = await weatherService.GetWeatherAsync(country, city);
//                  System.Diagnostics.Debug.WriteLine(city);
//                  System.Diagnostics.Debug.WriteLine(weather);
//              }
//          };
        }

        async void HandleClick (object sender, EventArgs e)
        {
            var weatherService = new WeatherService();
            var country = "FINLAND";
            var cities = await weatherService.GetCitiesByCountryAsync(country, this);

            foreach (var city in cities)
            {
                System.Diagnostics.Debug.WriteLine(city);
                var weather = await weatherService.GetWeatherAsync (country, city, this);
                if (weather != null)
                {
                    System.Diagnostics.Debug.WriteLine(weather);
                }
            }
        }

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


