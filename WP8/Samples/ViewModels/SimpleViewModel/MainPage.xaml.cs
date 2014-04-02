using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleViewModel.Resources;
using SimpleViewModel.Core;

namespace SimpleViewModel
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MyViewModel viewModel = new MyViewModel();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.button.Click += this.viewModel.Toggle;
            this.DataContext = this.viewModel;
        }

        ~MainPage()
        {
            this.viewModel.Finish();
        }
    }
}