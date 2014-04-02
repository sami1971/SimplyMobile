// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace CanvasDemo.iOS
{
    [Register ("LabelCell")]
    partial class LabelCell
    {
        [Outlet]
        MonoTouch.UIKit.UISlider sliderY { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextField textLabel { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextField textY { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (textLabel != null) {
                textLabel.Dispose ();
                textLabel = null;
            }

            if (sliderY != null) {
                sliderY.Dispose ();
                sliderY = null;
            }

            if (textY != null) {
                textY.Dispose ();
                textY = null;
            }
        }
    }
}
