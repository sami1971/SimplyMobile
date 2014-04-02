using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SimplyMobile.Location.Bing;
using SimplyMobile.IoC;
using SimplyMobile.Text;
using SimplyMobile.Web;
using ModernHttpClient;

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

            var bingClient = DependencyResolver.Current.GetService<BingClient>();

            var response = await bingClient.Get (47.64054, -122.12934);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.Diagnostics.Debug.WriteLine(response.Content);

                foreach (var set in response.Value.ResourceSets.SelectMany(a=>a.Resources))
                {
                    System.Diagnostics.Debug.WriteLine(set.Name);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(response.Error.Message);
            }
        }
    }
}

