using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace SimplyMobile.Controls
{
    [Register("SignatureView")]
    public class SignatureView : UIView
    {
        private UIColor underlineColor = UIColor.Black;
        private float underlineWidth = 2f;

        public UIColor UnderlineColor
        {
            get
            {
                return underlineColor;
            }
            set
            {
                underlineColor = value;
            }
        }


        public float UnderlineWidth
        {
            get
            {
                return underlineWidth;
            }
            set
            {
                underlineWidth = value;
            }
        }

        public SignatureView ()
        {
        }

        public override void Draw(System.Drawing.RectangleF rect)
        {
            base.Draw (rect);

            var context = UIGraphics.GetCurrentContext();

            context.SetLineWidth (this.UnderlineWidth);
            context.SetStrokeColor (this.UnderlineColor.CGColor);

            context.AddLines (new PointF[] { 
                new PointF (rect.Bottom - 2, rect.Left - 2),
                new PointF (rect.Bottom - 2, rect.Right - 2)
            });

            context.DrawPath (MonoTouch.CoreGraphics.CGPathDrawingMode.Stroke);
        }
    }
}

