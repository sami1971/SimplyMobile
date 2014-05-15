using System;
using MonoTouch.UIKit;
using SimplyMobile.Core;

namespace SimplyMobile.Navigation
{
    public abstract class NavigationController : Navigator
    {
        #region INavigationController implementation
        public override bool NavigateTo<T>(object sender, T model)
        {
            if (base.NavigateTo (sender, model))
            {
                return true;
            }

            this.LastSender = new WeakReference (sender);

            UIViewController newViewController;
            bool animated;

            if (!this.TryGetViewController (model, out newViewController, out animated))
            {
                return false;
            }

            UIViewController controller;


            var view = sender as UIView;

            if (view != null)
            {
                controller = view.GetController ();
            } 
            else
            {
                controller = sender as UIViewController;
            }

            if (controller != null)
            {
                if (controller.NavigationController == null)
                {
                    controller.PresentViewController (newViewController, animated, null);
                }
                else
                {
                    controller.NavigationController.PushViewController (newViewController, animated);
                }

                return true;
            }

            var navigationController = sender as UINavigationController;

            if (navigationController != null)
            {
                navigationController.PushViewController (newViewController, animated);
                return true;
            }

            return false;
        }
        #endregion

        protected WeakReference LastSender;

        protected abstract bool TryGetViewController<T>(T model, out UIViewController controller, out bool animated) where T : ViewModel;
    }
}

