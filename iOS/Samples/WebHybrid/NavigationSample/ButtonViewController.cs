using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Web;
using SimplyMobile.IoC;
using SimplyMobile.Navigation;

namespace NavigationSample
{
    public partial class ButtonViewController : UIViewController
    {
        private NavigationViewModel model;

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
            
            var webHybrid = new WebHybrid (this.webView, new SimplyMobile.Text.ServiceStack.JsonSerializer());

            string homePageUrl = NSBundle.MainBundle.BundlePath + "/Content/ButtonClicks.html";

            this.webView.LoadRequest (new NSUrlRequest (new NSUrl (homePageUrl, false)));

            this.model = new NavigationViewModel (
                Resolver.GetService<INavigationController> (),
                webHybrid);
//            webHybrid.RegisterCallback ("openNativeView", idString =>
//            {
//                int id;
//                if (int.TryParse(idString, out id))
//                {
//                    var newViewController = new NewViewController(id);
//                    this.NavigationController.PushViewController(newViewController, true);
//                }
//                else
//                {
//                    // notify something here
//                }
//            });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear (animated);

            this.model.BindViewOwner (this);
        }

        public override void ViewWillDisappear(bool animated)
        {
            this.model.UnbindViewOwner ();
            base.ViewWillDisappear (animated);
        }
    }
}

