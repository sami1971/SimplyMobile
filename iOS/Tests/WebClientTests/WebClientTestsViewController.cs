using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Web;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web.CanvasJs;
using System.Collections.ObjectModel;
using SimplyMobile.Core;
using SimplyMobile.Data;

namespace WebClientTests
{
    public partial class WebClientTestsViewController : UIViewController
    {
        private WebHybrid webHybrid;

        public static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public WebClientTestsViewController ()
            : base (UserInterfaceIdiomIsPhone ? "WebClientTestsViewController_iPhone" : "WebClientTestsViewController_iPad", null)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.

            string homePageUrl = NSBundle.MainBundle.BundlePath + "/Content/home.html";

            var serializer = new JsonSerializer ();
            this.webHybrid = new WebHybrid (this.webView, serializer);

            this.webHybrid.RegisterCallback ("test", Console.WriteLine);

            this.buttonSendScript.TouchUpInside += (object sender, EventArgs e) => 
            {
                //this.webHybrid.InjectJavaScript("RunMyItem();");
                //this.webHybrid.CallJsFunction("alert", "test");
            };

//          this.buttonSendScript.OnClick ((s, e) => {
//
//
//              this.webHybrid.CallJsFunction ("onViewModelData", model);
//
//
//          });

            this.webView.LoadRequest (new NSUrlRequest (new NSUrl (homePageUrl, false)));


            var model = ChartViewModel.Dummy;

            var dataSource = new ObservableDataSource<DataPoint> ()
            {
                Data = model.DataPoints
            };

            dataSource.Bind(this.datapointTable);

            foreach( var point in model.DataPoints) point.PropertyChanged += (s,e) => this.webHybrid.CallJsFunction ("onViewModelData", model);
//          if (this.canvasView != null) 
//          {
//              var canvas = new CanvasView (this.canvasView, serializer);
//              canvas.Load ();
//
//              var model = new ColumnModel () {
//                  theme = "theme2",
//                  title = new Title()
//                  {
//                      text = "Canvas Demo"
//                  },
//                  data = new ColumnData()
//                  {
//                      dataPoints = new ObservableCollection<DataPoint>(
//                          new[] {new DataPoint() { label = "Banana", y = 10 }}
//                      )
//                  }
//              };
//
//              canvas.SetModel (model);
//          }
        }
    }
}

