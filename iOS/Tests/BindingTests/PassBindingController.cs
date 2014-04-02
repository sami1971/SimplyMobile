using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BindingTests
{
    public partial class PassBindingController : UIViewController
    {
        private ExtensionViewModel model;

        static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public PassBindingController (ExtensionViewModel model)
            : base (UserInterfaceIdiomIsPhone ? "PassBindingController_iPhone" : "PassBindingController_iPad", null)
        {
            this.model = model;
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
        }
    }
}

