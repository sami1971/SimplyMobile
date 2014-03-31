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
    }
}
