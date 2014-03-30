using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Web;
using SimplyMobile.Text.ServiceStack;

namespace NavigationSample
{
    public partial class NewViewController : UIViewController
    {
        private readonly int id;

        public NewViewController (int id) : base ("NewViewController", null)
        {
            this.id = id;
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

            this.label.Text = string.Format ("Called from Button {0}", this.id);
        }
            
        protected override void Dispose(bool disposing)
        {
            base.Dispose (disposing);
        }
    }
}

