using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Core;
using System.Collections.Generic;
using System.ComponentModel;
using SimplyMobile.iOS;

namespace BindingTests
{
    public partial class BindingTestsViewController : BindingViewController
    {
        private ExtensionViewModel model;

        static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public BindingTestsViewController (ExtensionViewModel model)
            : base(UserInterfaceIdiomIsPhone ? "BindingTestsViewController_iPhone" : "BindingTestsViewController_iPad", null)
        {
            this.model = model ?? new ExtensionViewModel();
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

            if (this.PresentingViewController != null)
            {
                System.Diagnostics.Debug.WriteLine (this.PresentingViewController);
            }

            // Perform any additional setup after loading the view, typically from a nib.


            this.buttonNewView.TouchUpInside += (sender, e) => 
            {
                var newController = new BindingTestsViewController(this.model);
                this.PresentViewController(newController, true, null);
            };

            this.buttonClose.TouchUpInside += (sender, e) =>
            {
                 this.DismissViewController (true, null);
            };
        }

        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);

            this.AddDisappearAction(this.label.Bind(model, "TextField"));

            this.AddDisappearAction(this.textField.Bind(model, "TextField"));

            this.AddDisappearAction(this.button.BindTitle(model, "ButtonTitle"));

            this.button.OnClick(model.OnButtonClick);

            if (UserInterfaceIdiomIsPhone)
            {
                return;
            }

            this.AddDisappearAction(this.toggle1.Bind(model, "ToggleOn"));
            this.AddDisappearAction(this.toggle2.Bind(model, "ToggleOn"));

            this.AddDisappearAction(this.slider1.Bind(model, "SliderValue"));
            this.AddDisappearAction(this.slider2.Bind(model, "SliderValue"));

            this.AddDisappearAction(this.labelSliderValue.Bind(model, "SliderValueText"));
        }

        public override void ViewWillDisappear (bool animated)
        {
            base.ViewWillDisappear (animated);
        }

    }
}

