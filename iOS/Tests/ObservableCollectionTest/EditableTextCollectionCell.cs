using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ObservableCollectionTest
{
	public partial class EditableTextCollectionCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("EditableTextCollectionCell");
		public static readonly UINib Nib;

		static EditableTextCollectionCell ()
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
				Nib = UINib.FromName ("EditableTextCollectionCell_iPhone", NSBundle.MainBundle);
			else
				Nib = UINib.FromName ("EditableTextCollectionCell_iPad", NSBundle.MainBundle);
		}

		public EditableTextCollectionCell (IntPtr handle) : base (handle)
		{
		}

		public static EditableTextCollectionCell Create ()
		{
			return (EditableTextCollectionCell)Nib.Instantiate (null, null) [0];
		}
	}
}

