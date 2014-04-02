using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BitmapTest
{
    [Register("BitmapView")]
    public class BitmapView : UIImageView
    {
        public BitmapView (IntPtr handle) : base(handle)
        {
            //this.Image = UIImage.FromBundle ("jbX7P");
            this.AddObserver (this, new NSString("Frame"), NSKeyValueObservingOptions.New, IntPtr.Zero);


        }

        public override void Draw (System.Drawing.RectangleF rect)
        {
            base.Draw (rect);
            var baseMap = UIImage.FromBundle ("jbX7P");
            var maskMap = UIImage.FromBundle ("YdAei");

//          maskMap.CIImage.ImageByApplyingTransform(new MonoTouch.CoreGraphics.CGAffineTransform(
//
//          var copy = maskMap.CGImage.WithMask (baseMap.CGImage);
//          var i = UIImage.FromImage (copy);
//          var image = UIImage.FromImage (baseMap.CGImage);
            //baseMap.Draw (rect); 
            this.Image = baseMap;

            System.Diagnostics.Debug.WriteLine (baseMap);
            System.Diagnostics.Debug.WriteLine (maskMap);

        }

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
            // remove observers
        }

        public override void ObserveValue (NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {
            base.ObserveValue (keyPath, ofObject, change, context);


        }
    }
}

