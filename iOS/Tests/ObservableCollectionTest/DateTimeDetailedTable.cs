using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

namespace ObservableCollectionTest
{
    [Register("DateTimeDetailedTable")]
    public class DateTimeDetailedTable : UITableView, ITableCellProvider<DateTime>
    {
        public DateTimeDetailedTable ()
        {
        }

        public DateTimeDetailedTable (IntPtr handle) : base (handle)
        {

        }

        #region ITableCellProvider implementation

        public UITableViewCell GetCell (DateTime item)
        {
            throw new NotImplementedException ();
        }

        public float GetHeightForRow(NSIndexPath indexPath, DateTime item)
        {
            throw new NotImplementedException ();
        }

        #endregion
    }
}

