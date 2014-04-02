using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace NavigationSample
{
    using SimplyMobile;

    public partial class NewViewPage : PhoneApplicationPage
    {
        NewItemViewModel model;

        public NewViewPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.model = this.GetViewModel<NewItemViewModel>();
            this.label.Text = string.Format("Called from Button {0}", this.model.Id);
        }
    }
}