//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using Android.App;
using Android.Widget;
using Android.OS;
using SimplyMobile.Core;
using SimplyMobile.Device;
using Android.Util;
using System.Collections.Generic;

namespace DeviceTests
{
	[Activity (Label = "DeviceApp", MainLauncher = true)]
	public class MainActivity : ActivityCore
	{
		private TextView batteryLevel;
		private ToggleButton chargerState;
		private TextView accelerometerStatus;
//		private ToggleButton acceleroMeterState;

		protected override IEnumerable<MenuAction> MenuActions
		{
			get
			{
				return new [] 
				{
					new MenuAction("AP Networks", ()=> this.StartActivity<ApNetworkActivity>()),
					new MenuAction("Sensor measurement", ()=> this.StartActivity<SensorDelayActivity>()),
				};
			}
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

			var layout = new LinearLayout (this) 
			{
				Orientation = Orientation.Vertical
			};

			// Set our view from the "main" layout resource
			SetContentView (layout);

			this.batteryLevel = new TextView (this);

			layout.AddView (this.batteryLevel);

			chargerState = new ToggleButton (this) 
			{
				Enabled = false,
				TextOff = "Charger disconnected",
				TextOn = "Charger connected"
			};

			layout.AddView (chargerState);

			var accelometerLabel = new TextView (this) 
			{
				Text = "Accelerometer status"
			};

			layout.AddView (accelometerLabel);

			this.accelerometerStatus = new TextView (this);
			layout.AddView (this.accelerometerStatus);

			var textScreenSize = new TextView (this) 
			{
				Text = string.Format ("Screen size is {0}in.", Display.ScreenSizeInches)
			};

			layout.AddView (textScreenSize);
//			this.acceleroMeterState = new ToggleButton (this) 
//			{
//				TextOn = "Accelerometer ON",
//				TextOff = "Accelerometer OFF"
//			};
//
//			layout.AddView (this.acceleroMeterState);
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			// Get initial level, 0-100
			this.batteryLevel.Text = Battery.Level.ToString();

			// Subscribe to level changes. 
			Battery.OnLevelChange += HandleOnLevelChange;

			// Get the initial charger connection status
			this.chargerState.Checked = Battery.Charging;

			// Subscribe to charger status changes.
			Battery.OnChargerStatusChanged += HandleOnChargerStatusChanged;

			Accelometer.ReadingAvailable += HandleReadingAvailable;

			if (Accelometer.LatestStatus != null)
			{
				this.accelerometerStatus.Text = Accelometer.LatestStatus.ToString ();
			}

		}

		void HandleReadingAvailable (object sender, EventArgs<AccelometerStatus> e)
		{
			this.accelerometerStatus.Text = e.Value.ToString ();
		}

		void HandleOnChargerStatusChanged (object sender, EventArgs<bool> e)
		{
			this.chargerState.Checked = e.Value;
		}

		void HandleOnLevelChange (object sender, EventArgs<int> e)
		{
			this.batteryLevel.Text = e.Value.ToString();
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			Battery.OnLevelChange -= HandleOnLevelChange;
			Battery.OnChargerStatusChanged -= HandleOnChargerStatusChanged;
			Accelometer.ReadingAvailable -= HandleReadingAvailable;
		}
	}
}


