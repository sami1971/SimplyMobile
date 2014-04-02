using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace StackOverflowSamples
{
    public partial class SimpleTestViewController : UIViewController
    {
        private UIButton button;
        public SimpleTestViewController () : base ("SimpleTestViewController", null)
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

            button = new UIButton (new RectangleF(0, 100, 100, 50));
            button.BackgroundColor = UIColor.Red;
            button.TouchUpInside += (sender, e) => 
            {
                this.NavigationController.PushViewController(new SecondView(), true);
            };
            this.Add (button);

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }

    public partial class SecondView : UIViewController
    {
        private UIImageView _imageView;

        public SecondView () : base ("SecondView", null)
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

            _imageView = new UIImageView (new RectangleF(0,0, 200, 200));
            _imageView.Image = UIImage.FromFile ("Images/image.jpg");
            this.Add (_imageView);
            // Perform any additional setup after loading the view, typically from a nib.
        }
        protected override void Dispose (bool disposing)
        {
            System.Diagnostics.Debug.WriteLine ("Disposing "+this.GetType() + " hash code " + this.GetHashCode() + " disposing flag "+disposing);
            base.Dispose (disposing);
        }
    }
}

