using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web;
using WebClientTest.Resources;

namespace WebClientTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        private WebHybrid webHybrid;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.webHybrid = new WebHybrid(this.webView, new JsonSerializer());

            var fi = new FileInfo(@"./Assets/home.html");

            using (var streamReader = new StreamReader(fi.FullName))
            {
                this.webView.NavigateToString(streamReader.ReadToEnd());
            }

            this.webHybrid.RegisterCallback("test", s => System.Diagnostics.Debug.WriteLine(s));

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.webHybrid.CallJsFunction("RunMyItem");
            //this.webHybrid.CallJsFunction("window.external.notify", new Data() { Name = "Sami", Count = 8 });
        }
    }
}