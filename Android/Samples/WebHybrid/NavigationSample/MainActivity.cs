using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Web;
using Android.Webkit;
using SimplyMobile.IoC;
using SimplyMobile.Navigation;

namespace NavigationSample
{
    [Activity (Label = "NavigationSample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private NavigationViewModel model;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            DependencyResolver.Current.RegisterService<INavigationController, SampleNavigationController> ();

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var webView = FindViewById<WebView> (Resource.Id.webView1);
            var webHybrid = new WebHybrid(webView,
                new SimplyMobile.Text.RuntimeSerializer.JsonSerializer());

            webView.LoadUrl ("file:///android_asset/Content/ButtonClicks.html");

            this.model = new NavigationViewModel (
                Resolver.GetService<INavigationController> (),
                webHybrid);

//            this.webHybrid.LoadFromFile("/android_asset/Content/ButtonClicks.html");

//            this.webHybrid.RegisterCallback (
//                "openNativeView",
//                idString =>
//            {                
//                int id;
//                if (int.TryParse(idString, out id))
//                {
//                    var intent = new Intent(this, typeof(NewViewActivity));
//                    intent.PutExtra("id", id);
//                    this.StartActivity(intent);
//                }
//                else
//                {
//                    // notify something here
//                }
//                    
//                });
        }

        protected override void OnResume()
        {
            this.model.BindViewOwner (this);
            base.OnResume ();
        }

        protected override void OnPause()
        {
            this.model.UnbindViewOwner ();
            base.OnPause ();
        }
    }
}


