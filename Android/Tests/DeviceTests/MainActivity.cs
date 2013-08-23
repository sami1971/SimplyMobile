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
using SimplyMobile.Device;

namespace DeviceTests
{
	[Activity (Label = "DeviceApp", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private TextView batteryLevel;
		private ToggleButton chargerState;

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

			chargerState = new ToggleButton (this) {
				Enabled = false
			};

			layout.AddView (chargerState);

		}

		protected override void OnResume ()
		{
			base.OnResume ();

			this.batteryLevel.Text = Battery.Level.ToString();

			Battery.OnLevelChange += HandleOnLevelChange;

			this.chargerState.Checked = Battery.Charging;

			Battery.OnChargerStatusChanged += HandleOnChargerStatusChanged;
		}

		void HandleOnChargerStatusChanged (object sender, SimplyMobile.Core.EventArgs<bool> e)
		{
			this.chargerState.Checked = e.Value;
		}

		void HandleOnLevelChange (object sender, SimplyMobile.Core.EventArgs<int> e)
		{
			this.batteryLevel.Text = e.Value.ToString();
		}


		protected override void OnPause ()
		{
			base.OnPause ();
			Battery.OnLevelChange -= HandleOnLevelChange;
			Battery.OnChargerStatusChanged -= HandleOnChargerStatusChanged;
		}
	}
}


