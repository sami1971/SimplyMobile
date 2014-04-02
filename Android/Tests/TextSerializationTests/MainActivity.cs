using System.Reflection;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using System;
using System.IO;

namespace TextSerializationTests
{
    [Activity (Label = "TextSerializationTests", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            // tests can be inside the main assembly
            AddTest (Assembly.GetExecutingAssembly ());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate (bundle);

            var path = FileLoadTests.GetDicosPath ();

            using (var asset = Assets.Open ("TestData/dicos.json.txt"))
            using (var dest = File.Create (path)) 
            {
                asset.CopyTo (dest);
            }

            path = FileLoadTests.GetTinyPath ();

            using (var asset = Assets.Open ("TestData/tiny.json.txt"))
            using (var dest = File.Create (path)) 
            {
                asset.CopyTo (dest);
            }

            path = FileLoadTests.GetHighlyNestedPath ();

            using (var asset = Assets.Open ("TestData/_oj-highly-nested.json.txt"))
            using (var dest = File.Create (path)) 
            {
                asset.CopyTo (dest);
            }

        }
    }
}

