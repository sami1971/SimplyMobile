using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ObservableCollectionTest
{
	public partial class EditableTextTableCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("EditableTextTableCell");
		public static readonly UINib Nib;

		static EditableTextTableCell ()
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
				Nib = UINib.FromName ("EditableTextTableCell_iPhone", NSBundle.MainBundle);
			else
				Nib = UINib.FromName ("EditableTextTableCell_iPad", NSBundle.MainBundle);
		}

		public EditableTextTableCell (IntPtr handle) : base (handle)
		{
		}

		public static EditableTextTableCell Create ()
		{
			return (EditableTextTableCell)Nib.Instantiate (null, null) [0];
		}
	}
}

