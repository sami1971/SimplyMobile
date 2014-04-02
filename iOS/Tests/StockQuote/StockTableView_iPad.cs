using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;


namespace StockQuote
{
    [Register("StockTableView_iPad")]
    public class StockTableView_iPad : UITableView, ITableCellProvider<Stock>
    {
        private const string CellId = "StockTableView_iPad";

        public StockTableView_iPad ()
        {

        }

        public StockTableView_iPad (IntPtr handle) : base (handle)
        {

        }

        #region ITableCellProvider implementation

        public UITableViewCell GetCell (Stock stock)
        {
            var newCell = this.DequeueReusableCell(StockCell.Key) as StockCell;

            if (newCell == null)
            {
                newCell = StockCell.Create();
            }

            newCell.Bind (stock);

            return newCell;
        }

        public float GetHeightForRow(NSIndexPath indexPath, Stock item)
        {
            return 145f;
        }

        #endregion
    }
}

