using Microsoft.Phone.Tasks;
using SimplyMobile.Core;
using SimplyMobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;

namespace SimplyMobile.Device
{
    public class WindowsPhone : IPhone
    {
        public WindowsPhone()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged += DeviceNetworkInformation_NetworkAvailabilityChanged;
        }

        ~WindowsPhone()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged -= DeviceNetworkInformation_NetworkAvailabilityChanged;
        }

        public string CellularProvider
        {
            get
            {
                return DeviceNetworkInformation.CellularMobileOperator;
            }
        }

        public bool? IsCellularDataEnabled
        {
            get
            {
                return DeviceNetworkInformation.IsCellularDataEnabled;
            }
        }

        public bool? IsCellularDataRoamingEnabled
        {
            get
            {
                return DeviceNetworkInformation.IsCellularDataRoamingEnabled;
            }
        }

        public bool? IsNetworkAvailable
        {
            get
            {
                return DeviceNetworkInformation.IsNetworkAvailable;
            }
        }

        public string ICC
        {
            get { return string.Empty; }
        }

        public string MCC
        {
            get { return string.Empty; }
        }

        public string MNC
        {
            get { return string.Empty; }
        }

        public NetworkInterfaceInfo CurrentNetwork
        {
            get;
            private set;
        }

        public void DialNumber(string number)
        {
            new PhoneCallTask() { PhoneNumber = number }.Show();
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.NotificationType);
            System.Diagnostics.Debug.WriteLine(e.NetworkInterface.InterfaceName);
            System.Diagnostics.Debug.WriteLine(e.NetworkInterface.Description);
            System.Diagnostics.Debug.WriteLine(e.NetworkInterface.InterfaceState);
            System.Diagnostics.Debug.WriteLine(e.NetworkInterface.Bandwidth);

            if (e.NotificationType == NetworkNotificationType.InterfaceConnected)
            {
                this.CurrentNetwork = e.NetworkInterface;
            }
            else if (e.NotificationType == NetworkNotificationType.InterfaceDisconnected && e.NetworkInterface == this.CurrentNetwork)
            {
                this.CurrentNetwork = null;
            }
        }
    }
}
