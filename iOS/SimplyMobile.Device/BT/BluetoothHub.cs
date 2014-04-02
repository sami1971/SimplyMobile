using System;
using MonoTouch.CoreBluetooth;
using MonoTouch.CoreFoundation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class BluetoothHub : CBCentralManagerDelegate, IBluetoothHub
    {
        private const string uuid = "00001101-0000-1000-8000-00805f9b34fb";

        private CBCentralManager manager;

        public BluetoothHub ()
        {
            this.manager = new CBCentralManager (this, DispatchQueue.MainQueue);
        }

        #region IBluetoothHub implementation
        public bool Enabled
        {
            get
            {
                throw new NotImplementedException ();
            }
        }

        public async Task<IReadOnlyList<IBluetoothDevice>> GetPairedDevices()
        {
            return await Task.Factory.StartNew (() =>
            {
                var devices = new List<IBluetoothDevice> ();

                var action = new EventHandler<CBPeripheralsEventArgs> ((s, e) =>
                    devices.AddRange (e.Peripherals.Select (a => new BluetoothDevice (a))));

                this.manager.RetrievedPeripherals += action;
                this.manager.RetrieveConnectedPeripherals (new CBUUID[]{ CBUUID.FromString (uuid) });
                this.manager.RetrievedPeripherals -= action;

                return devices;
            });
        }

        public void OpenSettings()
        {
            throw new NotImplementedException ();
        }

        #endregion

        #region implemented abstract members of CBCentralManagerDelegate

        public override void UpdatedState(CBCentralManager central)
        {
            // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
            // NOTE: Don't call the base implementation on a Model class
//            throw new NotImplementedException ();
        }

        #endregion
    }
}

