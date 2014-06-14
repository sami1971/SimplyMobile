using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public interface IPhone
    {
        string CellularProvider { get; }

        bool? IsCellularDataEnabled { get; }

        bool? IsCellularDataRoamingEnabled { get; }

        bool? IsNetworkAvailable { get; }

        /// <summary>
        /// Gets the ISO Country Code
        /// </summary>
        string ICC { get; }

        /// <summary>
        /// Gets the Mobile Country Code
        /// </summary>
        string MCC { get; }

        /// <summary>
        /// Gets the Mobile Network Code
        /// </summary>
        string MNC { get; }

        void DialNumber(string number);
    }
}
