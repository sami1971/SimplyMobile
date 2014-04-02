using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;

namespace SimplyMobile.Device
{
    public class BluetoothHub : IBluetoothHub
    {
        public bool Enabled
        {
            get { return PeerFinder.AllowBluetooth; }
        }

        public async Task<IReadOnlyList<IBluetoothDevice>> GetPairedDevices()
        {
            PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";
            var devices = await PeerFinder.FindAllPeersAsync();
            return devices.Select(a => new BluetoothDevice(a)).ToList();
        }

        public void OpenSettings()
        {
            new ConnectionSettingsTask() { ConnectionSettingsType = ConnectionSettingsType.Bluetooth }.Show();
        }
    }
}
