using System;
using MonoTouch.UIKit;
using SimplyMobile.Core;

namespace SimplyMobile.Navigation
{
    public abstract class NavigationController : INavigationController
    {
        #region INavigationController implementation
        public bool NavigateTo<T>(object sender, T model) where T : SimplyMobile.Core.ViewModel
        {
            UIViewController newViewController;
            bool animated;

            if (!this.TryGetViewController (model, out newViewController, out animated))
            {
                return false;
            }

            var controller = sender as UIViewController;

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

        protected abstract bool TryGetViewController<T>(T model, out UIViewController controller, out bool animated) where T : ViewModel;
    }
}

