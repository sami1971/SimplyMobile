using System;
using Android.Content;
using Android.App;

namespace SimplyMobile
{
    using Core;

    public static class NavigationExtensions
    {
        public static bool StartActivity<T,TModel>(this Context context, TModel model) 
            where TModel: ViewModel where T : ViewModelActivity<TModel>
        {
            var guid = ViewModelContainer.Push (model);
            var intent = new Intent(context, typeof(T));
            intent.PutExtra ("modelId", guid.ToString ());

            if (!(context is Activity))
            {
                intent.AddFlags (ActivityFlags.NewTask);
            }

            context.StartActivity (intent);

            return true;
        }
    }
}

