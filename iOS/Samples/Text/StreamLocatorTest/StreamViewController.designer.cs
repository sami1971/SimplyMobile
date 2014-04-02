// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace StreamLocatorTest
{
    [Register ("StreamViewController")]
    partial class StreamViewController
    {
        [Outlet]
        MonoTouch.UIKit.UITextView textAppFolder { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextView textCurrentFolder { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (textAppFolder != null) {
                textAppFolder.Dispose ();
                textAppFolder = null;
            }

            if (textCurrentFolder != null) {
                textCurrentFolder.Dispose ();
                textCurrentFolder = null;
            }
        }
    }
}
