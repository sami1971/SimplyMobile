using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;

using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Text;

namespace XamarinFormsToText.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            var page = XamarinFormsToText.App.GetMainPage();
            var serializer =
                //new SimplyMobile.Text.ServiceStack.JsonSerializer();
                new SimplyMobile.Text.RuntimeSerializer.JsonSerializer();
                ;

            serializer.AddKnownType<Label>();
            // serializer= new SimplyMobile.Text.ServiceStack.JsonSerializer();
            var str = serializer.Serialize(page);
            //var toolbarItems = serializer.Serialize(page.ToolbarItems);
            var p2 = serializer.Deserialize<Xamarin.Forms.Page>(str);



            //p2.ToolbarItems = new System.Collections.ObjectModel.ObservableCollection<ToolbarItem>( serializer.Deserialize<List<ToolbarItem>>(toolbarItems));
            //serializer = new SimplyMobile.Text.ServiceStack.XmlSerializer();

            //str = serializer.Serialize(page);
            //p2 = serializer.Deserialize<Xamarin.Forms.Page>(str);

            //serializer = new SimplyMobile.Text.RuntimeSerializer.XmlSerializer();
            //str = serializer.Serialize(page);
            //p2 = serializer.Deserialize<Xamarin.Forms.Page>(str);

            Content = p2.ConvertPageToUIElement(this);
            //Content = page.ConvertPageToUIElement(this);
        }
    }
}
