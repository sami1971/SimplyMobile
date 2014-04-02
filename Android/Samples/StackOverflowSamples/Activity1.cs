using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Text.ServiceStack;
using System.IO;

namespace StackOverflowSamples
{
    [Activity(Label = "StackOverflowSamples", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private static GenericDto dto = new GenericDto();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate 
            { 
                button.Text = string.Format("{0} clicks!", ++dto.Count); 
                var serializer = new JsonSerializer();

                var intent = new Intent(this, typeof(SerializationSampleActivity));

                var b = new Bundle();
                b.PutString("object", serializer.Serialize(dto));
                intent.PutExtras(b);

                this.StartActivity(intent);

            };


        }

        public static void WriteToSdcard(object obj, string fileName)
        {
            if (Directory.Exists ("/sdcard"))
            {
                using (var writer = new StreamWriter (Path.Combine("/sdcard", fileName)))
                {
                    var serializer = new System.Runtime.Serialization.DataContractSerializer (obj.GetType ());
                    serializer.WriteObject (writer.BaseStream, obj);
                }
            }
        }
    }
}

