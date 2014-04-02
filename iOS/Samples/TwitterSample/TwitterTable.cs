using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

namespace TwitterSample
{
    [Register("TwitterTable")]
    public class TwitterTable : UITableView, ITableCellProvider<Datum>
    {
        public TwitterTable ()
        {
        }

        public TwitterTable (IntPtr handle) : base(handle)
        {
        }

        #region ITableCellProvider implementation

        public UITableViewCell GetCell (Datum item)
        {
            var newCell = this.DequeueReusableCell(TwitterTableCell.Key) 
                as TwitterTableCell ?? TwitterTableCell.Create();

            newCell.Bind (item);

            return newCell;
        }

        public float GetHeightForRow(NSIndexPath indexPath, Datum item)
        {
            return 114f;
        }

        #endregion
    }
}

