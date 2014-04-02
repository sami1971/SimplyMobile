// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ObservableCollectionTest
{
    [Register ("EditableTextTableCell")]
    partial class EditableTextTableCell
    {
        [Outlet]
        MonoTouch.UIKit.UISwitch switchCheck { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextField textField { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (switchCheck != null) {
                switchCheck.Dispose ();
                switchCheck = null;
            }

            if (textField != null) {
                textField.Dispose ();
                textField = null;
            }
        }
    }
}
