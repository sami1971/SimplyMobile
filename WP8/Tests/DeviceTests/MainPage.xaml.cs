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

namespace DeviceTests
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            this.batteryLevel.Text = Battery.Level.ToString();

            Battery.OnLevelChange += BatteryOnOnLevelChange;

            this.chargerStatus.IsChecked = Battery.Charging;

            Battery.OnChargerStatusChanged += BatteryChargerStatusChanged;
        }

        private void BatteryChargerStatusChanged(object sender, EventArgs<bool> eventArgs)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.chargerStatus.IsChecked = eventArgs.Value;
            });
        }

        private void BatteryOnOnLevelChange(object sender, EventArgs<float> eventArgs)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.batteryLevel.Text = eventArgs.Value.ToString();
            });
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}