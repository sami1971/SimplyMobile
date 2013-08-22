using System;
using MonoTouch.UIKit;
using System.Drawing;
using SimplyMobile.Core;
using SimplyMobile.Device;

namespace DeviceTests
{
	public class MainViewController : UIViewController
	{
		private UILabel batteryLevel;
		private UISwitch chargerStatus;

		public MainViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			batteryLevel = new UILabel (new RectangleF (0, 0, this.View.Frame.Width, 20)) {
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};

			this.View.AddSubview (batteryLevel);

			chargerStatus = new UISwitch (new RectangleF (
				(this.View.Frame.Width - 50) / 2, 25, 50, 20)) {
				AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
			};

			this.View.AddSubview (chargerStatus);


		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			this.batteryLevel.Text = Battery.Level.ToString ();
			Battery.OnLevelChange += HandleOnLevelChange;

			this.chargerStatus.On = Battery.Charging;
			Battery.OnChargerStatusChanged += HandleOnChargerStatusChanged;
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			Battery.OnLevelChange -= HandleOnLevelChange;

			Battery.OnChargerStatusChanged -= HandleOnChargerStatusChanged;
		}

		void HandleOnChargerStatusChanged (object sender, EventArgs<bool> e)
		{
			this.chargerStatus.On = e.Value;
		}

		void HandleOnLevelChange (object sender, EventArgs<float> e)
		{
			this.batteryLevel.Text = e.Value.ToString ();
		}
	}
}

