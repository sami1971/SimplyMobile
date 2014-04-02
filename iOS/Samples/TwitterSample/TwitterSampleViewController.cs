using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.IoC;
using SimplyMobile.Text;
using System.IO;

using SimplyMobile.Data;

namespace TwitterSample
{
    public partial class TwitterSampleViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public TwitterSampleViewController ()
            : base (UserInterfaceIdiomIsPhone ? "TwitterSampleViewController_iPhone" : "TwitterSampleViewController_iPad", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.

            var localFile = Path.Combine (NSBundle.MainBundle.BundlePath, "TwitterJson.txt");

            TwitterResponse response;

            using (var reader = new StreamReader (localFile))
            {
                var serializer = DependencyResolver.Current.GetService<IJsonSerializer> ();

                response = serializer.DeserializeFromReader<TwitterResponse>(reader);
            }

            if (response != null)
            {
                var dataSource = new ObservableDataSource<Datum>(response.data);
                dataSource.Bind (this.table);
            }

//          serializer.Deserialize<TwitterResponse>()
        }
    }
}

