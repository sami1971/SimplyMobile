using System;
using MonoTouch.UIKit;
using SimplyMobile.Core;

namespace DeviceTests
{
    public class TabBarController : UITabBarController
    {
        UIViewController mainController;

        public TabBarController ()
        {


        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad ();

            this.mainController = new MainViewController () 
            {
                TabBarItem = new UITabBarItem (UITabBarSystemItem.Featured, 0)
            };

            if (MobileApp.IsPhone)
            {
                var deviceController = new DeviceFeatureViewController () {
                    TabBarItem = new UITabBarItem (UITabBarSystemItem.Contacts, 1)
                };

                this.ViewControllers = new UIViewController[] 
                {
                    mainController,
                    deviceController
                };
            }
            else
            {
                this.ViewControllers = new UIViewController[] 
                {
                    mainController
                };
            }


        }
    }
}

