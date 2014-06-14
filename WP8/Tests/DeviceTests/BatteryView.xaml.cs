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

using System.Windows;
using SimplyMobile.Core;
using SimplyMobile.Device;
using SimplyMobile.IoC;

namespace DeviceTests
{
    public partial class BatteryView
    {
        // Constructor
        public BatteryView()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            this.batteryLevel.Text = Battery.Level.ToString();

            Battery.OnLevelChange += BatteryOnOnLevelChange;

            this.chargerStatus.IsChecked = Battery.Charging;
            this.chargerStatus.IsEnabled = false;

            Battery.OnChargerStatusChanged += BatteryChargerStatusChanged;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var device = Resolver.GetService<IDevice>();

            this.buttonCall.IsEnabled = device.Phone != null;
        }

        private void BatteryChargerStatusChanged(object sender, EventArgs<bool> eventArgs)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.chargerStatus.IsChecked = eventArgs.Value;
            });
        }

        private void BatteryOnOnLevelChange(object sender, EventArgs<int> eventArgs)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.batteryLevel.Text = eventArgs.Value.ToString();
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Resolver.GetService<IDevice>().Phone.DialNumber(this.phoneNumber.Text);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var btHub = Resolver.GetService<IDevice>().BluetoothHub;

            if (btHub != null)
            {
                var devices = await btHub.GetPairedDevices();

                foreach (var d in devices)
                {
                    System.Diagnostics.Debug.WriteLine(d.Name);
                }

                btHub.OpenSettings();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new System.Uri("/DeviceInfoPage.xaml", System.UriKind.Relative));
        }
    }
}