using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Text.ServiceStack;
using System.IO;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace StackOverflowSamples
{
    [Activity(Label = "StackOverflowSamples", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private static GenericDto dto = new GenericDto();
        private bool wasClicked;

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

            Drawable blankDrawable = Resources.GetDrawable (Resource.Drawable.Icon);
            var blankBitmap = ((BitmapDrawable)blankDrawable).Bitmap;
            var pixel1 = blankBitmap.GetPixel (100, 100);

            var colorOfPixel = new Color (pixel1);
            System.Diagnostics.Debug.WriteLine (pixel1);
            System.Diagnostics.Debug.WriteLine (colorOfPixel);

            var alpha = (byte)(pixel1 >> 3 * 8);
            var red = (byte)(pixel1 >> 2 * 8);
            var green = (byte)(pixel1 >> 8);
            var blue = (byte)pixel1;

            System.Diagnostics.Debug.WriteLine (alpha);
            System.Diagnostics.Debug.WriteLine (red);
            System.Diagnostics.Debug.WriteLine (green);
            System.Diagnostics.Debug.WriteLine (blue);

            button.Click += HandleClick;
        }

        void HandleClick (object sender, EventArgs e)
        {
            this.wasClicked = true;
            // load image
        }


        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean ("clicked", this.wasClicked);
            base.OnSaveInstanceState (outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            this.wasClicked = savedInstanceState.GetBoolean ("clicked");
            if (this.wasClicked)
            {
                HandleClick (this, null);
            }

            base.OnRestoreInstanceState (savedInstanceState);
        }

        public static void WriteToSdcard(object obj, string fileName)
        {
            if (Directory.Exists ("/sdcard"))
            {
                using (var writer = new StreamWriter (System.IO.Path.Combine("/sdcard", fileName)))
                {
                    var serializer = new System.Runtime.Serialization.DataContractSerializer (obj.GetType ());
                    serializer.WriteObject (writer.BaseStream, obj);
                }
            }
        }
    }
}

