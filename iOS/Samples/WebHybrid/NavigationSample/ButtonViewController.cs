using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Web;
using SimplyMobile.Text.ServiceStack;

namespace NavigationSample
{
    public partial class ButtonViewController : UIViewController
    {
        public ButtonViewController () : base ("ButtonViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad ();
			
            var webHybrid = new WebHybrid (this.webView, new JsonSerializer());

            string homePageUrl = NSBundle.MainBundle.BundlePath + "/Content/ButtonClicks.html";

            this.webView.LoadRequest (new NSUrlRequest (new NSUrl (homePageUrl, false)));

            webHybrid.RegisterCallback ("openNativeView", idString =>
            {
                int id;
                if (int.TryParse(idString, out id))
                {
                    var newViewController = new NewViewController(id);
                    this.NavigationController.PushViewController(newViewController, true);
                }
                else
                {
                    // notify something here
                }
            });
        }
    }
}

