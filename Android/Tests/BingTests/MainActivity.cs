using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Location.Bing;
using SimplyMobile.Text.ServiceStack;

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
				var bingClient = new BingClient(
                    "Apcl0Dzk-uwuqlIpDPjGLaA0oHXERDiGBuE3Vzxx3peRCr8gmSRPr-J6cij7U1pZ",
                    new JsonSerializer());

				var response = await bingClient.Get(47.64054,-122.12934);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					System.Diagnostics.Debug.WriteLine(response.Value.Copyright);
					CreateIntent(response.Value);
				}
			};
		}

		private void CreateIntent(BingResponse response)
		{
			var nMgr = this.GetSystemService(NotificationService) as NotificationManager;
			var notification = new Notification(Resource.Drawable.Icon, "Incoming Location Info");
			var intent = new Intent(this, typeof(NewActivity));
			var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
			notification.SetLatestEventInfo(this, "Your location has changed", "Location info", pendingIntent);
			nMgr.Notify(0, notification);
		}
	}
}


