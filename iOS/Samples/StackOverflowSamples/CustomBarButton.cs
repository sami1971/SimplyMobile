using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

using MonoTouch.CoreImage;

namespace StackOverflowSamples
{
    public class CustomBarButton : UIBarButtonItem
    {
        public new event EventHandler Clicked
        {
            add
            {
//              base.Clicked += value;
            }
            remove
            {
//              base.Clicked -= value;
            }
        }

        public static RectangleF GetInfinite()
        {
            var image = CIImage.EmptyImage;

            if (image.Extent.IsInfinite ())
            {
                return image.Extent;
            }

            throw new Exception ("Unable to create infinite rect");
        }
    }
}

