using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    using Core;

    public abstract class NavigatorViewModel : ViewModel
    {
        protected NavigatorViewModel(INavigationController navigator)
        {
            this.Navigator = navigator;
        }

        /// <summary>
        /// The presenter for the this view model
        /// </summary>
        /// <remarks>Should be used to navigate from this ViewModel to another using INavigationController</remarks>
        public object Presenter { get; set; }

        public readonly INavigationController Navigator;
    }
}
