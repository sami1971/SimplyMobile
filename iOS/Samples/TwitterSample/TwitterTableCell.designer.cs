// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace TwitterSample
{
    [Register ("TwitterTableCell")]
    partial class TwitterTableCell
    {
        [Outlet]
        MonoTouch.UIKit.UIButton buttonViewComments { get; set; }

        [Outlet]
        MonoTouch.UIKit.UIImageView image { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelCaption { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelType { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelUser { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (image != null) {
                image.Dispose ();
                image = null;
            }

            if (labelCaption != null) {
                labelCaption.Dispose ();
                labelCaption = null;
            }

            if (labelUser != null) {
                labelUser.Dispose ();
                labelUser = null;
            }

            if (labelType != null) {
                labelType.Dispose ();
                labelType = null;
            }

            if (buttonViewComments != null) {
                buttonViewComments.Dispose ();
                buttonViewComments = null;
            }
        }
    }
}
