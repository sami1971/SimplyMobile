using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ObservableCollectionTest
{
    public partial class DateTimeCollectionCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString ("DateTimeCollectionCell");
        public static readonly UINib Nib;

        static DateTimeCollectionCell ()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                Nib = UINib.FromName ("DateTimeCollectionCell_iPhone", NSBundle.MainBundle);
            else
                Nib = UINib.FromName ("DateTimeCollectionCell_iPad", NSBundle.MainBundle);
        }

        public DateTimeCollectionCell (IntPtr handle) : base (handle)
        {
        }

        public static DateTimeCollectionCell Create ()
        {
            return (DateTimeCollectionCell)Nib.Instantiate (null, null) [0];
        }
    }
}

