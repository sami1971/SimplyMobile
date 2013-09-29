using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SimplyMobile.Data
{
	public interface ITableCellProvider
	{
		UITableViewCell GetCell(object item);
		float GetHeightForRow (NSIndexPath indexPath);
	}
}

