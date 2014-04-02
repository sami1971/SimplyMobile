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
using SimplyMobile;

namespace NavigationSample
{
    [Activity (Label = "NewViewActivity")]          
    public class NewViewActivity : ViewModelActivity<NewItemViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.newView);

//            FindViewById<TextView>(Resource.Id.textView1).Text =
//                string.Format("Called from Button {0}'", Intent.Extras.GetInt ("id"));
        }

        protected override void OnResume()
        {
            FindViewById<TextView>(Resource.Id.textView1).Text = 
                string.Format("Called from Button {0}'", this.Model.Id);
            base.OnResume ();
        }
    }
}

