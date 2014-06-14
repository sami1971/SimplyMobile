using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimplyMobile.Device;
using SimplyMobile.IoC;

namespace DeviceTests
{
    public partial class DeviceInfoPage : PhoneApplicationPage
    {
        public DeviceInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var device = Resolver.GetService<IDevice>();

            this.ModelPanel.Children.Add(new TextBlock() 
            { 
                Text = string.Format("{0} {1}", device.Manufacturer, device.Name)
            });

            this.ModelPanel.Children.Add(new TextBlock()
            {
                Text = string.Format("Hardware version: {0}", device.HardwareVersion)
            });

            this.ModelPanel.Children.Add(new TextBlock()
            {
                Text = string.Format("Firmware version: {0}", device.FirmwareVersion)
            });

            this.AddToModel("Current locale is {0}", CultureInfo.CurrentCulture.DisplayName);

            this.Resolution.Text =  string.Format("Screen resolution is {0}x{1}, {2}dpi", device.Screen.Width, device.Screen.Height, (int)device.Screen.Xdpi);
            this.Size.Text = string.Format("Screen size is {0:0.00}x{1:0.00}\", diagonal {2:0.00}\"", device.Screen.ScreenWidthInches(), device.Screen.ScreenHeightInches(), device.Screen.ScreenSizeInches());

            foreach (var member in device.GetType().GetMembers())
            {
                this.Properties.Children.Add(new TextBlock() { Text = member.Name });
            }

            this.Provider.Text = device.Phone.CellularProvider;
        }

        private void AddToModel(string info, params object[] p)
        {
            this.ModelPanel.Children.Add(new TextBlock() { Text = string.Format(info, p) });
        }
    }
}