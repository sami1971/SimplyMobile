using System;
using SimplyMobile.Data;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;

namespace StockQuote
{
    public class StockDataAdapter : ObservableDataSource
    { 
        public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            var item = this.Data[indexPath.Row] as Stock;

            if (item == null)
            {
                return base.GetCell (tableView, indexPath);
            }

            var cellProvider = tableView as ITableCellProvider;

            if (cellProvider != null)
            {
                return cellProvider.GetCell (item);
            }

            var cell = tableView.DequeueReusableCell(StockCell.Key) as StockCell;

            if (cell == null)
            {
                cell = StockCell.Create();
                //var views = NSBundle.MainBundle.LoadNib("StockTableCell", cell, null);
                //cell = Runtime.GetNSObject( views.ValueAt(0) ) as StockTableCell;
            }

            cell.Bind (item);

            return cell;
        }

        public StockDataAdapter ()
        {
            this.CellId = "StockTableCell";
        }
    }
}

