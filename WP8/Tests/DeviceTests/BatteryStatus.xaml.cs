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

using System.ComponentModel;
using System.Windows;
using SimplyMobile.Core;
using SimplyMobile.Device;

namespace DeviceTests
{
    /// <summary>
    /// Battery Status Control
    /// </summary>
    public partial class BatteryStatus
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BatteryStatus()
        {
            InitializeComponent();

            // if we are in designer mode, lets not initialize the 
            // battery status reading, otherwise our designer will
            // fail to display the control as it cannot initiate 
            // WP runtime components
            if (!DesignerProperties.IsInDesignTool)
            {
                Init();
            }
        }

        private void Init()
        {
            this.batteryLevel.Text = string.Format("{0:00}%", Battery.Level);

            Battery.OnLevelChange += OnBatteryLevelChanged;

            this.chargerStatus.IsChecked = Battery.Charging;
            this.chargerStatus.IsEnabled = false;

            Battery.OnChargerStatusChanged += OnChargerStatusChanged;
        }

        private void OnChargerStatusChanged(object sender, EventArgs<bool> eventArgs)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.chargerStatus.IsChecked = eventArgs.Value;
            });
        }

        private void OnBatteryLevelChanged(object sender, EventArgs<int> eventArgs)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.batteryLevel.Text = string.Format("{0:00}%", Battery.Level);
            });
        }
    }
}
