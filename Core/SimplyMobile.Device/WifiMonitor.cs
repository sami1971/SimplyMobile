using System;
using SimplyMobile.Core;

namespace SimplyMobile.Device
{
    public partial class WifiMonitor
    {
        private bool enabled;

        

        #region IWifiMonitor Members

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