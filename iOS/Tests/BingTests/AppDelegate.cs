using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.IoC;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web;
using System.Net.Http;
using ModernHttpClient;
using SimplyMobile.Location.Bing;

namespace BingTests
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        BingTestsViewController viewController;
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            var r = DependencyResolver.Current;
            r.RegisterService<IJsonSerializer, JsonSerializer>();
            // use iOS specific HttpClient for faster downloads
            r.RegisterService<HttpClient>(t => new HttpClient(new AFNetworkHandler()));
            r.RegisterService<IRestClient>(t => new JsonClient(t.GetService<HttpClient>(), t.GetService<IJsonSerializer>()));
            r.RegisterService<BingClient>(t => new BingClient(BingKey.AppKey, t.GetService<IRestClient>()));

            window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            viewController = new BingTestsViewController ();
            window.RootViewController = viewController;
            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}

