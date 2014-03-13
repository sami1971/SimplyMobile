using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimplyMobile.Web
{
    public interface IReachability
    {
        bool HasInternetConnection { get; }

        Task<bool> IsHostReachable(string host, TimeSpan timeout);
    }
}
