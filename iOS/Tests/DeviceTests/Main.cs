using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace DeviceTests
{
    /// <summary>
    /// The application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// This is the main entry point of the application.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        static void Main (string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main (args, null, "DeviceApp");
        }
    }
}
