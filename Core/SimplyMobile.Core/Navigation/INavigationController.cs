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

        void SetDelegate<T>(Func<T, bool> func) where T : ViewModel;
        bool RemoveDelegates<T>() where T : ViewModel;

        void SetWeakDelegate<T>(Func<T, bool> func) where T : ViewModel;
        //bool RemoveWeakDelegate<T>(Func<T, bool> func) where T : ViewModel;
    }
}
