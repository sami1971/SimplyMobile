using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace WebClientTest
{
	[Activity (Label = "WebClientTest", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private Button button;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += async delegate
			{
				await this.VeryLongTask();
			};
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			button.Click += HandleClick;
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			button.Click -= HandleClick;
		}

		private void HandleClick (object sender, EventArgs e)
		{

		}

		private async Task VeryLongTask()
		{
			await Task.Factory.StartNew (() =>
				{
					// long running code here
				});
		} 
	}
}


