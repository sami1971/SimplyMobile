using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.IoC;
using SQLite.Net.Interop;

using SQLite.Net.Platform.XamarinIOS;
using SQLiteBlobTests;
using SimplyMobile.Device;

namespace DeviceTests
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("DeviceApp")]
    public partial class DeviceApp
    {
        /// <summary>
        /// The window.
        /// </summary>
        UIWindow window;

        /// <summary>
        /// The controller.
        /// </summary>
        private MainViewController controller;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            this.window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            // If you have defined a root view controller, set it here:
            this.controller = new MainViewController();

            this.window.RootViewController = this.controller;
            
            // make the window visible
            this.window.MakeKeyAndVisible();
            DependencyResolver.Current.RegisterService<IDevice>(t => AppleDevice.CurrentDevice ());
            DependencyResolver.Current.RegisterService<ISQLitePlatform, SQLitePlatformIOS>();
            DependencyResolver.Current.RegisterService<IPhone, PhoneImplementation> ();
            this.OnStart();

            Resolver.GetService<StoreAccelerometerData>().Start();
            Resolver.GetService<StoreLocationChange> ().Start ();

            var device = Resolver.GetService<IDevice> ();
            System.Diagnostics.Debug.WriteLine (string.Format("Device {0} a phone.", device.Phone != null ? "is" : "is not"));
            System.Diagnostics.Debug.WriteLine (device.Screen);
            System.Diagnostics.Debug.WriteLine (device.Name);
            System.Diagnostics.Debug.WriteLine (device.HardwareVersion);
            System.Diagnostics.Debug.WriteLine (device.FirmwareVersion);
            return true;
        }

        public override void WillTerminate(UIApplication application)
        {
            Resolver.GetService<StoreAccelerometerData>().Stop();
            Resolver.GetService<StoreLocationChange> ().Stop ();
            base.WillTerminate(application);
        }

        public override void DidEnterBackground(UIApplication application)
        {

        }

        public override void WillEnterForeground(UIApplication application)
        {

        }
    }
}

