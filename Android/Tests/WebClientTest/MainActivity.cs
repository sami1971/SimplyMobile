using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using SimplyMobile.Web;
using Android.Webkit;
using SimplyMobile.Text;
using SimplyMobile.Core;
using SimplyMobile;

namespace WebClientTest
{
    [Activity (Label = "WebClientTest", MainLauncher = true)]
    public class MainActivity : ActivityCore
    {
        private Button button;
        private WebHybrid webHybrid;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button> (Resource.Id.myButton);

            var serializer = new SimplyMobile.Text.ServiceStack.JsonSerializer();



            var webView = FindViewById<WebView> (Resource.Id.webView1);

            this.webHybrid = new WebHybrid (webView, serializer);

            this.webHybrid.RegisterCallback ("dataCallback", Console.WriteLine);

            webView.LoadUrl("file:///android_asset/Content/home.html");

            button.Click += delegate
            {
                var data = new Data()
                {
                    Name = "Sami",
                    Count = 99
                };

                this.webHybrid.InjectJavaScript(string.Format("Native(\"test\", {0})", serializer.Serialize(data)));

                this.webHybrid.CallJsFunction("Native", "test", data);

                this.webHybrid.CallJsFunction("alert", "test");
            };
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            button.Click += HandleClick;
        }

        protected override void OnPause ()
        {
            base.OnPause ();
            button.Click -= HandleClick;
        }

        private void HandleClick (object sender, EventArgs e)
        {

        }

        private async Task VeryLongTask()
        {
            await Task.Factory.StartNew (() =>
                {
                    // long running code here
                });
        } 
    }
}


