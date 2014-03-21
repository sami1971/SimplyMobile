using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Web
{
    public class Reachability : IReachability
    {
        public Task<bool> IsHostReachable(string host, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public bool HasInternetConnection
        {
            get { return NetworkInterface.GetIsNetworkAvailable(); }
        }
    }
}
