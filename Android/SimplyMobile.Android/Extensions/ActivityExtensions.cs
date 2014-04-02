using System;
using SimplyMobile.Core;
using Android.App;

namespace SimplyMobile
{
    public static class ActivityExtensions
    {
        public static T GetViewModel<T>(this Activity activity) where T : ViewModel
        {
            return ViewModelContainer.Pull (activity.Intent.Extras.GetString ("modelId")) as T;
        }
    }
}

