using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Core;

namespace BindingTests
{
	public partial class BindingTestsViewController : UIViewController
	{
		private ExtensionViewModel model;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public BindingTestsViewController ()
			: base (UserInterfaceIdiomIsPhone ? "BindingTestsViewController_iPhone" : "BindingTestsViewController_iPad", null)
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

			this.model = new ExtensionViewModel ();

			// Perform any additional setup after loading the view, typically from a nib.
			this.label.Bind (model, "TextField");
			this.textField.Bind (model, "TextField");

			this.button.OnClick(model.OnButtonClick);
			this.button.BindTitle(model, "ButtonTitle");
		}
	}
}

