using System;
using SimplyMobile.Navigation;
using MonoTouch.UIKit;
using SimplyMobile.Core;

namespace NavigationSample
{
    public class SampleNavigationController : NavigationController
    {
        #region implemented abstract members of NavigationController

        protected override bool TryGetViewController<T>(T model, out UIViewController controller, out bool animated)
        {
            animated = true;

            var newViewModel = model as NewItemViewModel;
            if (newViewModel != null)
            {
                controller = new NewViewController (newViewModel);
                return true;
            }

            controller = null;
            return false;
        }

        #endregion


    }
}

