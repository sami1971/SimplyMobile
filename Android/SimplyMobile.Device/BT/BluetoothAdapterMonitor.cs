using System;
using SimplyMobile.Core;
using Android.Content;
using Android.Bluetooth;

namespace SimplyMobile.Device.BT
{
    public class BluetoothAdapterMonitor : Monitor
    {
        public event EventHandler<EventArgs<BluetoothDevice>> OnDiscovery;
        public event EventHandler<EventArgs<DateTime>> OnDiscoveryEnded;
        public event EventHandler<EventArgs<AdapterStatus>> OnStatusChanged;

        public bool EnableBluetooth()
        {
            throw new NotImplementedException ();
        }

        protected override IntentFilter Filter
        {
            get
            {
                IntentFilter filter = new IntentFilter();
                filter.AddAction(BluetoothDevice.ActionFound);
                filter.AddAction(BluetoothAdapter.ActionDiscoveryStarted);
                filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
                filter.AddAction(BluetoothAdapter.ActionStateChanged);
                return filter;
            }
        }

        public override void OnReceive (Context context, Intent intent)
        {
            switch (intent.Action)
            {
            case BluetoothDevice.ActionFound:
                this.OnDiscovery.Invoke (this, intent.GetParcelableExtra (BluetoothDevice.ExtraDevice) as BluetoothDevice);
                break;
            case BluetoothAdapter.ActionDiscoveryFinished:
                this.OnDiscoveryEnded.Invoke (this, DateTime.Now);
                break;
            case BluetoothAdapter.ActionStateChanged:
                this.OnStatusChanged.Invoke (this, (AdapterStatus)intent.GetIntExtra (BluetoothAdapter.ExtraState, -1));
                break;
            }
        }
    }
}

