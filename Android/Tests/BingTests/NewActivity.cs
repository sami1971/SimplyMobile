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

namespace BingTests
{
    [Activity (Label = "NewActivity")]          
    public class NewActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            var layout = new LinearLayout (this);

            this.SetContentView (layout);
            // Create your application here
            var button = new Button (this);

            layout.AddView (button);
        }
    }
}

