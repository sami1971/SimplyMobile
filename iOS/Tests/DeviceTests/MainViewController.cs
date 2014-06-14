using System;
using System.Collections.ObjectModel;
using MonoTouch.UIKit;
using System.Drawing;
using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Device;
using SimplyMobile.IoC;

namespace DeviceTests
{
    /// <summary>
    /// The main view controller.
    /// </summary>
    public class MainViewController : UIViewController
    {
        private UILabel batteryLevel;
//      private UISwitch chargerStatus;
//      private UISwitch audioCapture;
//        private UITableView tableView;

        private UILabel accelerometerStatus;

        public MainViewController ()
        {

        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.batteryLevel = new UILabel (new RectangleF (0, 25, this.View.Frame.Width, 20)) {
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
                BackgroundColor = UIColor.White
            };

            this.View.AddSubview (batteryLevel);

//          this.chargerStatus = new UISwitch (new RectangleF ((this.View.Frame.Width - 50) / 2, 50, 50, 20)) 
//          {
//              AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
//          };
//
//          this.View.AddSubview (chargerStatus);

            this.accelerometerStatus = new UILabel (new RectangleF (0, 250, this.View.Frame.Width, 20)) {
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
                BackgroundColor = UIColor.White
            };

            this.View.AddSubview (accelerometerStatus);

//          this.audioCapture = new UISwitch (new RectangleF ((this.View.Frame.Width - 50) / 2, 50, 50, 20)) 
//          {
//              AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
//          };
//
//          this.View.AddSubview (this.audioCapture);

//            this.tableView = new UITableView(new RectangleF(0, 75, this.View.Frame.Width, this.View.Frame.Height - 80))
//            {
//                AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin
//            };
//
//            this.View.AddSubview(this.tableView);
//            DeviceApp.BatteryStatus.Bind(this.tableView);


        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);

            this.batteryLevel.Text = Battery.Status.ToString();
            Battery.OnLevelChange += HandleOnLevelChange;

            Battery.OnChargerStatusChanged += HandleOnChargerStatusChanged;

            Accelometer.ReadingAvailable += HandleReadingAvailable;

            var device = Resolver.GetService<IDevice>();

            System.Diagnostics.Debug.WriteLine(string.Format("Device screen width is {0} inches.", device.Screen.ScreenWidthInches()));
            System.Diagnostics.Debug.WriteLine(string.Format("Device screen height is {0} inches.", device.Screen.ScreenHeightInches()));
            System.Diagnostics.Debug.WriteLine(string.Format("Device screen size is {0} inches.", device.Screen.ScreenSizeInches()));
        }

        public override void ViewDidDisappear (bool animated)
        {
            base.ViewDidDisappear (animated);

            Battery.OnLevelChange -= HandleOnLevelChange;

            Battery.OnChargerStatusChanged -= HandleOnChargerStatusChanged;
            Accelometer.ReadingAvailable -= HandleReadingAvailable;
        }

        void HandleOnChargerStatusChanged (object sender, EventArgs<bool> e)
        {
            this.batteryLevel.Text = Battery.Status.ToString ();
        }

        void HandleOnLevelChange (object sender, EventArgs<int> e)
        {
            this.batteryLevel.Text = Battery.Status.ToString ();
        }

        void HandleReadingAvailable (object sender, EventArgs<AccelometerStatus> e)
        {
            this.accelerometerStatus.Text = e.Value.ToString ();
        }
    }
}

