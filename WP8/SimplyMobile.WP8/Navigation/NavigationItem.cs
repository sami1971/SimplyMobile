using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    public class NavigationItem
    {
        public NavigationItem(string address, UriKind uriKind)
        {
            this.Address = address;
            this.UriKind = uriKind;
        }

        public string Address { get; private set; }
        public UriKind UriKind { get; private set; }
    }
}
