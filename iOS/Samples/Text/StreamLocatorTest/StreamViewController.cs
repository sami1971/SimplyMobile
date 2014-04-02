using MonoTouch.UIKit;

using SimplyMobile.IoC;
using SimplyMobile.Text;

namespace StreamLocatorTest
{
    public partial class StreamViewController : UIViewController
    {
        public StreamViewController () : base ("StreamViewController", null)
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
            
            // Perform any additional setup after loading the view, typically from a nib.

            var l = Resolver.GetService<IStreamLocator> ();

            this.textCurrentFolder.Text = l.CurrentPath;
            this.textAppFolder.Text = l.AppFolder;
        }
    }
}

