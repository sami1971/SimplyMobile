using System;
using System.Collections.ObjectModel;
using MonoTouch.UIKit;
using System.Drawing;
using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Device;

namespace DeviceTests
{
    /// <summary>
    /// The main view controller.
    /// </summary>
    public class MainViewController : UIViewController
	{
		private UILabel batteryLevel;
		private UISwitch chargerStatus;
		private UISwitch audioCapture;
        private UITableView tableView;
		
        public MainViewController ()
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.batteryLevel = new UILabel (new RectangleF (0, 0, this.View.Frame.Width, 20)) {
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};

			this.View.AddSubview (batteryLevel);

			this.chargerStatus = new UISwitch (new RectangleF ((this.View.Frame.Width - 50) / 2, 25, 50, 20)) 
			{
				AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
			};

			this.View.AddSubview (chargerStatus);

			this.audioCapture = new UISwitch (new RectangleF ((this.View.Frame.Width - 50) / 2, 50, 50, 20)) 
			{
				AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
			};

			this.View.AddSubview (this.audioCapture);

            this.tableView = new UITableView(new RectangleF(0, 75, this.View.Frame.Width, this.View.Frame.Height - 80))
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
            };

            this.View.AddSubview(this.tableView);
            DeviceApp.BatteryStatus.Bind(this.tableView);
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

		void HandleOnLevelChange (object sender, EventArgs<int> e)
		{
			this.batteryLevel.Text = e.Value.ToString ();
		}
	}
}

