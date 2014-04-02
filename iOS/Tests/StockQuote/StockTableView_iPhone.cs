using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Data;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;

namespace StockQuote
{
    [Register("StockTableView_iPhone")]
    public class StockTableView_iPhone : UITableView, ITableCellProvider<Stock>
    {
        private const string CellId = "StockTableView_iPhone";

        public StockTableView_iPhone ()
        {
        }

        public StockTableView_iPhone (IntPtr handle) : base (handle)
        {

        }

        #region ITableCellProvider implementation

        public UITableViewCell GetCell (Stock item)
        {
            var cell = this.DequeueReusableCell(CellId);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, CellId);
            }

            cell.TextLabel.Text = item.ToString();
            return cell;
        }

        public float GetHeightForRow(NSIndexPath indexPath, Stock item)
        {
            return 25f;
        }

        #endregion
    }
}

