using System;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public partial class WifiMonitor
    {
        private bool enabled;

        public event EventHandler<EventArgs<bool>> OnActiveChanged;

        public event EventHandler<EventArgs<Exception>> OnException;

        public bool Active { get; private set; }

        #region IWifiMonitor Members

        public string CurrentSSID { get; set; }
        public bool Enabled
        {
            get
            {
#if DEBUG
                return enabled;
#else
                throw new NotImplementedException();
#endif
            }
        }

        public bool TrySetState(bool enabled)
        {
            this.enabled = enabled;

            return true;
        }

        #endregion

        #region IMonitor Members


        public bool Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}