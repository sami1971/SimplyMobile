using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace StackOverflowSamples
{
    [Activity (Label = "TaskActivity")]         
    public class TaskActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            // Create your application here
        }

        protected override async void OnResume()
        {
            base.OnResume ();

            StartAnimate ();

            await RunAsync (TimeSpan.FromSeconds (4));

            StopAnimate ();
        }

        private async Task RunAsync(TimeSpan span)
        {
            await Task.Delay (span);
        }

        private void StartAnimate()
        {
            // put animation here
        }

        private void StopAnimate()
        {
            // stop animating after the thread has ended
        }
    }
}

