using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.IoC;
using SimplyMobile.Web;
using System.Net.Http;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;

namespace TwitterSample
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        TwitterSampleViewController viewController;
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);

            DependencyResolver.Current.RegisterService<IJsonSerializer, JsonSerializer> ();

            viewController = new TwitterSampleViewController ();
            window.RootViewController = viewController;
            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}

