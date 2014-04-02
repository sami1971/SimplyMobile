using SimplyMobile.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationSample
{
    public class SampleNavigationController : NavigationController
    {
        protected override bool TryGetAddress<T>(T model, out string address, out UriKind uriKind)
        {
            uriKind = UriKind.Relative;

            if (typeof(T) == typeof(NewItemViewModel))
            {
                address = "/NewViewPage.xaml";
                return true;
            }

            address = string.Empty;
            
            return false;
        }
    }
}
