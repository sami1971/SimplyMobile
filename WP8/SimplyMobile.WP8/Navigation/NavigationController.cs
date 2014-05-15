using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    using Core;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    public abstract class NavigationController : Navigator
    {
        public override bool NavigateTo<T>(object sender, T model)
        {
            if (base.NavigateTo<T>(sender, model))
            {
                return true;
            }

            var page = sender as Page;

            if (page == null)
            {
                return false;
            }

            string address;
            UriKind uriKind;

            if (this.Items != null && this.Items.ContainsKey(typeof(T)))
            {
                var navItem = this.Items[typeof(T)];
                return page.Navigate<T>(navItem.Address, navItem.UriKind, model);
            }

            if (this.TryGetAddress(model, out address, out uriKind))
            {
                return page.Navigate<T>(address, uriKind, model);
            }

            return false;
        }

        protected Dictionary<Type, NavigationItem> Items;

        protected abstract bool TryGetAddress<T>(T model, out string address, out UriKind uriKind) where T : ViewModel;


    }
}
