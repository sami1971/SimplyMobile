using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SimplyMobile.Data
{
    public class CollectionCellDelegate<T>: ICollectionCellProvider<T>
    {
        public delegate UICollectionViewCell GetCellDelegate(T item, NSIndexPath indexPath);

        private readonly GetCellDelegate getCell;

        public CollectionCellDelegate (GetCellDelegate getCell)
        {
            this.getCell = getCell;
        }

        #region ICollectionCellProvider implementation

        public UICollectionViewCell GetCell(T item, NSIndexPath indexPath)
        {
            return this.getCell (item, indexPath);
        }

        #endregion
    }
}

