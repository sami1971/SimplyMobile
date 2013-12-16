using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Web;
using SimplyMobile.Text.ServiceStack;

namespace WebClientTests
{
	public partial class WebClientTestsViewController : UIViewController
	{
		private WebHybrid webHybrid;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public WebClientTestsViewController ()
			: base (UserInterfaceIdiomIsPhone ? "WebClientTestsViewController_iPhone" : "WebClientTestsViewController_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			string homePageUrl = NSBundle.MainBundle.BundlePath + "/Content/home.html";

			this.webHybrid = new WebHybrid (this.webView, new JsonSerializer ());

			this.webHybrid.RegisterCallback ("test", (data) => 
				{
					Console.WriteLine(data);
				}
			);

			this.buttonSendScript.TouchUpInside += (object sender, EventArgs e) => 
			{
				this.webHybrid.InjectJavaScript("RunMyItem();");
				this.webHybrid.CallJsFunction("alert", "test");
			};

			this.webView.LoadRequest (new NSUrlRequest (new NSUrl (homePageUrl, false)));
		}
	}
}

