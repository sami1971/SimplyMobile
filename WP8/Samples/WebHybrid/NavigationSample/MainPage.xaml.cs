using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NavigationSample.Resources;
using SimplyMobile.Web;
using SimplyMobile.IoC;
using SimplyMobile.Navigation;

namespace NavigationSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IWebHybrid webHybrid;
        private NavigationViewModel model;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            this.webHybrid = new WebHybrid(this.webView, new SimplyMobile.Text.JsonNet.JsonSerializer());

            this.webView.Navigate(new Uri("HTML/ButtonClicks.html", UriKind.Relative));

            this.model = new NavigationViewModel(Resolver.GetService<INavigationController>(), this.webHybrid);

            //this.model = new NavigationViewModel(
            //    new NavigationDelegate<NewItemViewModel>( model =>
            //    {

            //    }),
            //    this.webHybrid);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.model.BindViewOwner(this);

            //webHybrid.RegisterCallback(
            //    "openNativeView",
            //    idString => Resolver.GetService<INavigationController>().NavigateTo(this, new NewItemViewModel(idString)));
            //NavigationService.Navigate(new Uri("/NewViewPage.xaml?id=" + idString, UriKind.Relative)));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //webHybrid.RemoveCallback("openNativeView");

            this.model.UnbindViewOwner();
            base.OnNavigatedFrom(e);
        }
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}