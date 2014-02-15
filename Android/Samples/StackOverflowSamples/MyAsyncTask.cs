using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Android.Graphics.Drawables;

namespace StackOverflowSamples
{
    internal class LoadImage : AsyncTask<String, long, Bitmap>
    {
        protected override Bitmap RunInBackground(params string[] @params)
        {
            var myurl = new URL(@params[0]); 
            var bmp = BitmapFactory.DecodeStream(myurl.OpenConnection().InputStream);
            return bmp;
        }

//        protected override void OnPostExecute(Bitmap result)
//        {
//            item.icon = BitmapDescriptorFactory.FromBitmap(result);
//            item.Location = new LatLng(-41.227834, 174.812857);
//            item.Snippet = "Snippet2";
//            item.Title = "Title2";
//            item.ShowInfoWindowOnStartup = true;
//            _mapLocationList.Add(item);
//        }
    }
}