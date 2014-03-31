using System;
using SimplyMobile.Core;

namespace SimplyMobile
{
    public class ViewModelActivity<T> : ActivityCore where T : ViewModel
    {
        protected T Model;

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            this.Model = ViewModelContainer.Pull (this.Intent.Extras.GetString ("modelId")) as T;
        }
    }
}

