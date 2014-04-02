using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ObservableCollectionTest
{
    public partial class DateTimeTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString ("DateTimeTableCell");
        public static readonly UINib Nib;

        static DateTimeTableCell ()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                Nib = UINib.FromName ("DateTimeTableCell_iPhone", NSBundle.MainBundle);
            else
                Nib = UINib.FromName ("DateTimeTableCell_iPad", NSBundle.MainBundle);
        }

        public DateTimeTableCell (IntPtr handle) : base (handle)
        {
        }

        public static DateTimeTableCell Create ()
        {
            return (DateTimeTableCell)Nib.Instantiate (null, null) [0];
        }
    }
}

