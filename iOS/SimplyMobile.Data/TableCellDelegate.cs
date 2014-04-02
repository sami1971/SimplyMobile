using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SimplyMobile.Data
{
    public class TableCellDelegate<T> : ITableCellProvider<T>
    {
        public delegate UITableViewCell GetCellDelegate(T item);
        public delegate float GetHeightDelegate(int row, T item);

        private readonly GetCellDelegate getCell;
        private readonly GetHeightDelegate getHeight;

        public TableCellDelegate (GetCellDelegate getCell, GetHeightDelegate getHeight)
        {
            this.getCell = getCell;
            this.getHeight = getHeight;
        }

        #region ITableCellProvider implementation

        public UITableViewCell GetCell(T item)
        {
            return this.getCell (item);
        }

        public float GetHeightForRow(NSIndexPath indexPath, T item)
        {
            return this.getHeight (indexPath.Row, item);
        }

        #endregion
    }
}

