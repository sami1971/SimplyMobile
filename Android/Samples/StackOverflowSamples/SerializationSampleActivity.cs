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
using SimplyMobile.Text.ServiceStack;

namespace StackOverflowSamples
{
    [Activity (Label = "SerializationSampleActivity")]          
    public class SerializationSampleActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView(Resource.Layout.serialization_layout);

            var serializer = new JsonSerializer ();
            var str = this.Intent.Extras.GetString ("object");
            var obj = serializer.Deserialize<GenericDto> (str);

            FindViewById<TextView> (Resource.Id.textCount).Text = 
                string.Format ("Clicked {0} times.", obj.Count);
        }
    }
}

