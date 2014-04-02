// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace DeviceTests
{
    [Register ("DeviceFeatureViewController")]
    partial class DeviceFeatureViewController
    {
        [Outlet]
        MonoTouch.UIKit.UIButton buttonCall { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextField textPhoneNumber { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (textPhoneNumber != null) {
                textPhoneNumber.Dispose ();
                textPhoneNumber = null;
            }

            if (buttonCall != null) {
                buttonCall.Dispose ();
                buttonCall = null;
            }
        }
    }
}
