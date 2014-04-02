using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimplyMobile.Navigation
{
    using Core;

    public interface INavigationController
    {
        bool NavigateTo<T>(object sender, T model) where T : ViewModel;
    }
}
