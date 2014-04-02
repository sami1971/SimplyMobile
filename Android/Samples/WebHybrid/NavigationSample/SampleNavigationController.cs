using System;
using SimplyMobile.Navigation;
using Android.Content;

namespace NavigationSample
{
    public class SampleNavigationController : NavigationController
    {
        #region implemented abstract members of NavigationController

        protected override bool TryGetIntent<T>(T model, object sender, out Android.Content.Intent intent)
        {
            if (sender is Context && typeof(T) == typeof(NewItemViewModel))
            {
                intent = new Intent (sender as Context, typeof(NewViewActivity));
                return true;
            }

            intent = null;
            return false;
        }

        #endregion


    }
}

