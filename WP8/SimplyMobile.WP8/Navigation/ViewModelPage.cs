using Microsoft.Phone.Controls;
using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Navigation
{
    public class ViewModelPage<T> : PhoneApplicationPage where T : ViewModel
    {
        protected T Model;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Model = this.GetViewModel<T>();
        }
    }
}
