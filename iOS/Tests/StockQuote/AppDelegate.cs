using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Plugins.WcfStockService;
using SimplyMobile.Data;


namespace StockQuote
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        //private static IStockQuoteService stockClient;

        //public static ObservableDataSource Data
        //{
        //    get;
        //    private set;
        //}

        //public static IStockQuoteService StockClient
        //{
        //    get { return stockClient ?? (stockClient = new StockQuoteServiceClient()); }
        //}

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app,
                                                NSDictionary options)
        {
            //Data = new ObservableDataSource ();

            // create a new window instance based on the screen size
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            // If you have defined a root view controller, set it here:
            // window.RootViewController = myViewController;
            window.RootViewController = new NavigationController ();

            // make the window visible
            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}

