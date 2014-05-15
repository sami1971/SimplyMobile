using System;
using SimplyMobile.Core;
using Android.Content;

namespace SimplyMobile.Navigation
{
    public abstract class NavigationController : Navigator
    {
        #region INavigationController implementation

        public override bool NavigateTo<T>(object sender, T model)
        {
            if (base.NavigateTo<T> (sender, model))
            {
                return true;
            }

            Intent intent;

            if (TryGetIntent (model, sender, out intent))
            {
                var guid = ViewModelContainer.Push (model);

                intent.PutExtra ("modelId", guid.ToString ());
                sender.StartActivity (intent);

                return true;
            }

            return false;
        }

        #endregion

        protected abstract bool TryGetIntent<T>(T model, object sender, out Intent intent);
    }
}

