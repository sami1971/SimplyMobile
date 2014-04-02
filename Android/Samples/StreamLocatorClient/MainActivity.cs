using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using StreamLocatorDependency;
using SimplyMobile.Text;
using System.IO;
using SimplyMobile.IoC;

namespace StreamLocatorClient
{
    [Activity (Label = "StreamLocatorClient", MainLauncher = true)]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.myButton);
            
            var locator = new StreamLocator();

            button.Click += delegate
            {
                button.Text = string.Format ("{0} clicks!", count++);

                var l = new StreamWriterFunction(locator);

                var file = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), 
                    "myFile.txt");
                l.WriteToPath(file, button.Text);
            };

            FindViewById<TextView> (Resource.Id.textApp).Text = locator.AppFolder;
            FindViewById<TextView> (Resource.Id.textCurrent).Text = locator.CurrentPath;

        }
    }
}


