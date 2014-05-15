using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    using Core;

    public static class NavigationExtensions
    {
        public static bool Navigate(this NavigatorViewModel navigator, ViewModel model)
        {
            return navigator.Navigator.NavigateTo(navigator.Presenter, model);
        }
    }
}
