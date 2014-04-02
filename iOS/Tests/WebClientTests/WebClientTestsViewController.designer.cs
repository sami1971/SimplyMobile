// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace WebClientTests
{
    [Register ("WebClientTestsViewController")]
    partial class WebClientTestsViewController
    {
        [Outlet]
        MonoTouch.UIKit.UIButton buttonSendScript { get; set; }

        [Outlet]
        CanvasDemo.iOS.DataPointTable datapointTable { get; set; }

        [Outlet]
        MonoTouch.UIKit.UIWebView webView { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (buttonSendScript != null) {
                buttonSendScript.Dispose ();
                buttonSendScript = null;
            }

            if (webView != null) {
                webView.Dispose ();
                webView = null;
            }

            if (datapointTable != null) {
                datapointTable.Dispose ();
                datapointTable = null;
            }
        }
    }
}
