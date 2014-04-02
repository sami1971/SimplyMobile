using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

namespace StockQuote
{
    public class StockTableDelegate : UITableViewDelegate
    {
        public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
        {
            var cellProvider = tableView as ITableCellProvider;

            if (cellProvider != null)
            {
                return cellProvider.GetHeightForRow (indexPath);
            }

            return 145f;
        }
    }
}

