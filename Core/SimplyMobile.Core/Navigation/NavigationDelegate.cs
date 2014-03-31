using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    public class NavigationDelegate<T> where T : ViewModel
    {
        public delegate void NavigateDelegate(T model);

        private NavigateDelegate navigationDelegate;

        public NavigationDelegate(NavigateDelegate navigationDelegate)
        {
            this.navigationDelegate = navigationDelegate;
        }

        public void NavigateTo(T model)
        {
            this.navigationDelegate.Invoke(model);
        }
    }
}
