using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Web;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web.CanvasJs;
using System.Collections.ObjectModel;
using SimplyMobile.Core;

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

			var serializer = new JsonSerializer ();
			this.webHybrid = new WebHybrid (this.webView, serializer);

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

			this.buttonSendScript.OnClick ((s, e) => {
			});

			this.webView.LoadRequest (new NSUrlRequest (new NSUrl (homePageUrl, false)));

			if (this.canvasView != null) 
			{
				var canvas = new CanvasView (this.canvasView, serializer);
				canvas.Load ();

				var model = new ColumnModel () {
					theme = "theme2",
					title = new Title()
					{
						text = "Canvas Demo"
					},
					data = new ColumnData()
					{
						dataPoints = new ObservableCollection<DataPoint>(
							new[] {new DataPoint() { label = "Banana", y = 10 }}
						)
					}
				};

				canvas.SetModel (model);
			}
		}
	}
}

