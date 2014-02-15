using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Core;

namespace PassingData
{
    [Activity(Label = "My Activity")]
    public class Activity2 : Activity
    {
        private List<PropertyChangedEventHandler> handlers;
        private SimpleViewModel model;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            var serializer = new JsonSerializer();
            if (bundle != null)
            {
                this.model = serializer.Deserialize<SimpleViewModel>(bundle.GetString("model"));
            }

            var button = this.FindViewById<Button>(Resource.Id.buttonClose);

            button.Click += (sender, args) =>
                {
                    var intent = new Intent();
                    intent.PutExtra("model", serializer.Serialize(this.model));
                    this.SetResult(Result.Ok, intent);
                    this.Finish();
                };
        }

        protected override void OnResume()
        {
            base.OnResume();
            var editText = this.FindViewById<EditText>(Resource.Id.editText1);
            this.handlers = new List<PropertyChangedEventHandler> { editText.Bind(this.model, "Text") };
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.handlers.ForEach(a=>this.model.PropertyChanged -= a);
        }
    }
}