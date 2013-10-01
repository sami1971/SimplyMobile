using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;


namespace StockQuote
{
	[Register("StockTableView_iPad")]
	public class StockTableView_iPad : UITableView, ITableCellProvider
	{
		private const string CellId = "StockTableView_iPad";

		public StockTableView_iPad ()
		{

		}

		public StockTableView_iPad (IntPtr handle) : base (handle)
		{

		}

		#region ITableCellProvider implementation

		public UITableViewCell GetCell (object item)
		{
			var stock = item as Stock;

			if (stock == null)
			{
				var cell = new UITableViewCell(UITableViewCellStyle.Value1, "textCell");
				cell.TextLabel.Text = item.ToString();
				return cell;
			}

			var newCell = this.DequeueReusableCell(StockCell.Key) as StockCell;

			if (newCell == null)
			{
				newCell = StockCell.Create();
			}

			newCell.Bind (stock);

			return newCell;
		}

		public float GetHeightForRow (NSIndexPath indexPath)
		{
			return 145f;
		}

		#endregion
	}
}

