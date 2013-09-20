using Android.App;
using Android.OS;

namespace SimplyMobile.Core
{
    public abstract class ActivityCore : Activity
    {
        protected override void OnPause()
        {
            base.OnPause();
            var app = DependencyResolver.Current.GetService<MobileApp>();
            if (app != null)
            {
                app.StartActivityTransition(this);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            var app = DependencyResolver.Current.GetService<MobileApp>();
            if (app != null && app.WasBackgrounded)
            {
                OnResumeFromBackground();
            }
        }

        protected abstract void OnResumeFromBackground();
    }
}