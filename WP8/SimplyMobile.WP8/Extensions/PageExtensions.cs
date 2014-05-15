using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimplyMobile
{
    public static class PageExtensions
    {
        public static T GetViewModel<T>(this Page page) where T : ViewModel
        {
            Guid guid;

            if (page.NavigationContext.QueryString.ContainsKey("modelId") &&
                Guid.TryParse(page.NavigationContext.QueryString["modelId"], out guid))
            {
                return ViewModelContainer.Pull(guid) as T;
            }

            return null;
        }

        public static bool Navigate<T>(this Page page, string address, UriKind uriKind, T model) where T : ViewModel
        {
            var guid = ViewModelContainer.Push(model);
            var pageAddress = string.Format("{0}?modelId={1}", address, guid);
            return page.NavigationService.Navigate(new Uri(pageAddress, uriKind));
        }
    }
}
