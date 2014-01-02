using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SimplyMobile.Location.Bing;

namespace BingTests
{
	public partial class BingTestsViewController : UIViewController
	{
		public BingTestsViewController () : base ("BingTestsViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			var bingClient = new BingClient ("Apcl0Dzk-uwuqlIpDPjGLaA0oHXERDiGBuE3Vzxx3peRCr8gmSRPr-J6cij7U1pZ");

			var response = await bingClient.Get (47.64054, -122.12934);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				System.Diagnostics.Debug.WriteLine (response.Content);
			}
		}
	}
}

