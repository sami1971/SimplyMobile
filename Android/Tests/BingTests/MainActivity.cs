using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Location.Bing;

namespace BingTests
{
	[Activity (Label = "BingTests", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += async delegate
			{
				var bingClient = new BingClient("Apcl0Dzk-uwuqlIpDPjGLaA0oHXERDiGBuE3Vzxx3peRCr8gmSRPr-J6cij7U1pZ");

				var response = await bingClient.Get(47.64054,-122.12934);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					System.Diagnostics.Debug.WriteLine(response.Value.Copyright);
				}
			};
		}
	}
}


