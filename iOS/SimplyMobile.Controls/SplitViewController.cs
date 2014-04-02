using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace SimplyMobile.Controls
{
    public abstract class SplitViewController : UISplitViewController
    {
        public static readonly NSString NotificationWillChangeStatusBarOrientation = new NSString("UIApplicationWillChangeStatusBarOrientationNotification");
        public static readonly NSString NotificationDidChangeStatusBarOrientation = new NSString("UIApplicationDidChangeStatusBarOrientationNotification");     
        public static readonly NSString NotificationOrientationDidChange = new NSString("UIDeviceOrientationDidChangeNotification");
        public static readonly NSString NotificationFavoriteUpdated = new NSString("NotificationFavoriteUpdated");

        private static readonly NSString ApplicationStatusBarOrientationUserInfoKey = new NSString ("UIApplicationStatusBarOrientationUserInfoKey");

        private readonly NSObject ObserverWillRotate;
        private readonly NSObject ObserverDidRotate;

        protected SplitViewController ()
        {

            ObserverWillRotate = NSNotificationCenter.DefaultCenter.AddObserver(
                NotificationWillChangeStatusBarOrientation, 
                OnWillRotate);          
            ObserverDidRotate = NSNotificationCenter.DefaultCenter.AddObserver(
                NotificationDidChangeStatusBarOrientation, 
                OnDidRotate);       
        }

        protected abstract void AddNavigationButton(UIBarButtonItem barButton);
        protected abstract void RemoveNavigationButton();

        protected abstract void SetPopover(UIPopoverController popover);
        protected abstract void RemovePopover();

        protected override void Dispose (bool disposing)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(ObserverWillRotate);
            NSNotificationCenter.DefaultCenter.RemoveObserver(ObserverDidRotate);
            base.Dispose (disposing);
        }

        protected void OnWillRotate (NSNotification notification)
        {
            if (!IsViewLoaded) return;
            if (notification == null) return;

            var o1 = notification.UserInfo.ValueForKey(ApplicationStatusBarOrientationUserInfoKey);
            int o2 = Convert.ToInt32(o1.ToString ());
            UIInterfaceOrientation toOrientation =(UIInterfaceOrientation) o2;
            var isSelectedTab = (TabBarController.SelectedViewController == this);

            var duration = UIApplication.SharedApplication.StatusBarOrientationAnimationDuration;

            if (!isSelectedTab) 
            {
                base.WillRotate (toOrientation, duration);

                UIViewController master = ViewControllers[0];
                var theDelegate = Delegate;

                //YOU_DONT_FEEL_QUEAZY_ABOUT_THIS_BECAUSE_IT_PASSES_THE_APP_STORE
                var button = base.ValueForKey (new NSString("_barButtonItem")) as UIBarButtonItem;


                if (toOrientation == UIInterfaceOrientation.Portrait
                    || toOrientation == UIInterfaceOrientation.PortraitUpsideDown) 
                {
                    if (theDelegate != null && theDelegate.RespondsToSelector(new Selector("splitViewController:willHideViewController:withBarButtonItem:forPopoverController:"))) {
                        try {
                            UIPopoverController popover = base.ValueForKey(new NSString("_hiddenPopoverController")) as UIPopoverController;
                            theDelegate.WillHideViewController(this, master, button, popover);
                        } catch (Exception e) {
                            Console.WriteLine ("There was a nasty error while notifyng splitviewcontrollers of a portrait orientation change: " + e.Message);
                        }
                    }

                } else {
                    if (theDelegate != null && theDelegate.RespondsToSelector(new Selector("splitViewController:willShowViewController:invalidatingBarButtonItem:"))) {
                        try {
                            theDelegate.WillShowViewController (this, master, button);
                        } catch (Exception e) {
                            Console.WriteLine ("There was a nasty error while notifyng splitviewcontrollers of a landscape orientation change: " + e.Message);
                        }
                    }
                }
            }
        }

        protected void OnDidRotate (NSNotification notification)
        {
            if (!IsViewLoaded ||notification == null) return;

            var o1 = notification.UserInfo.ValueForKey(ApplicationStatusBarOrientationUserInfoKey);
            int o2 = Convert.ToInt32(o1.ToString ());
            UIInterfaceOrientation toOrientation =(UIInterfaceOrientation) o2;
            var isSelectedTab = (TabBarController.SelectedViewController == this);
            if (!isSelectedTab) {
                base.DidRotate(toOrientation);
            }
        }

        private class SplitViewDelegate : UISplitViewControllerDelegate
        {
            public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
            {
                return inOrientation == UIInterfaceOrientation.Portrait
                    || inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
            }

            public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
            {
                var dvc = svc as SplitViewController;

                if (dvc != null) 
                {
                    dvc.AddNavigationButton(barButtonItem);
                    dvc.SetPopover(pc);
                }
            }

            public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
            {
                var dvc = svc as SplitViewController;

                if (dvc != null) 
                {
                    dvc.RemoveNavigationButton();
                    dvc.RemovePopover ();
                }
            }
        }
    }


}

