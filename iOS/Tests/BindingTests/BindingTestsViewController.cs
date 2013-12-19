using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace BindingTests
{
	public partial class BindingTestsViewController : UIViewController
	{
		private ExtensionViewModel model;

		private List<PropertyChangedEventHandler> eventHandlers;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public BindingTestsViewController (ExtensionViewModel model)
			: base (UserInterfaceIdiomIsPhone ? "BindingTestsViewController_iPhone" : "BindingTestsViewController_iPad", null)
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

			this.eventHandlers = new List<PropertyChangedEventHandler>();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.eventHandlers = new List<PropertyChangedEventHandler>();

			this.eventHandlers.Add(this.label.Bind (model, "TextField"));

			this.eventHandlers.Add(this.textField.Bind (model, "TextField"));

	
			this.eventHandlers.Add(this.button.BindTitle(model, "ButtonTitle"));

			this.button.OnClick(model.OnButtonClick);

			if (UserInterfaceIdiomIsPhone)
			{
				return;
			}

			this.eventHandlers.Add (this.toggle1.Bind (model, "ToggleOn"));
			this.eventHandlers.Add (this.toggle2.Bind (model, "ToggleOn"));

			this.eventHandlers.Add(this.slider1.Bind(model, "SliderValue"));
			this.eventHandlers.Add(this.slider2.Bind(model, "SliderValue"));

			this.eventHandlers.Add (this.labelSliderValue.Bind(model, "SliderValueText"));
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			//model.Unbind();
			model.RemoveHandlers(this.eventHandlers);
		}

	}
}

