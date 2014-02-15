using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Text.ServiceStack;

namespace PassingData
{
    [Activity(Label = "PassingData", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.buttonOpen);
            var viewModel = new SimpleViewModel()
                {
                    Label = "Text",
                    Text = string.Empty
                };

            button.Click += delegate
                {
                    var serializer = new JsonSerializer();
                    var intent = new Intent(this, typeof(Activity2));
                    intent.PutExtra("model", serializer.Serialize(viewModel));
                    this.StartActivityForResult(intent, Resource.Id.buttonOpen);
                };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == Resource.Id.buttonOpen && resultCode == Result.Ok && data != null)
            {
                var serializer = new JsonSerializer();
                var viewModel = serializer.Deserialize<SimpleViewModel>(data.GetStringExtra("model"));
                this.FindViewById<TextView>(Resource.Id.textView1).Text = viewModel.Label;
                this.FindViewById<TextView>(Resource.Id.textView2).Text = viewModel.Text;
            }
        }
    }
}

