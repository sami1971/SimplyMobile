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

    public abstract class NavigationController : INavigationController
    {
        public bool NavigateTo<T>(object sender, T model) where T : ViewModel
        {
            var page = sender as Page;
            string address;
            UriKind uriKind;

            if (page != null && this.TryGetAddress(model, out address, out uriKind))
            {
                var guid = ViewModelContainer.Push(model);
                var pageAddress = string.Format("{0}?modelId={1}", address, guid);
                return page.NavigationService.Navigate(new Uri(pageAddress, uriKind));
            }

            return false;
        }

        protected abstract bool TryGetAddress<T>(T model, out string address, out UriKind uriKind) where T : ViewModel;
    }
}
