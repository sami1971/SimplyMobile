using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Bluetooth;
using System.Linq;

namespace SimplyMobile.Device
{
    public class BluetoothHub : IBluetoothHub
    {
        readonly BluetoothAdapter adapter;

        public bool Enabled
        {
            get
            {
                return this.adapter.IsEnabled;
            }
        }

        public BluetoothHub() : this(BluetoothAdapter.DefaultAdapter) {}

        public BluetoothHub(BluetoothAdapter adapter)
        {
            this.adapter = adapter;
        }

        #region IBluetoothHub implementation
        public async Task<IReadOnlyList<IBluetoothDevice>> GetPairedDevices()
        {
            return await Task.Factory.StartNew(()=>
               this.adapter.BondedDevices.Select (a => new AndroidBluetoothDevice (a)).ToList ());
        }

        public void OpenSettings()
        {
            this.StartActivityForResult (new Android.Content.Intent (BluetoothAdapter.ActionRequestEnable));
        }
        #endregion
    }
}

