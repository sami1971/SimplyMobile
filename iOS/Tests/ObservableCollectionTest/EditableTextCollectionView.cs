using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using SimplyMobile.Data;

namespace ObservableCollectionTest
{
    [Register("EditableTextCollectionView")]
    public class EditableTextCollectionView : UICollectionView, ICollectionCellProvider<EditableText>
    {
        public EditableTextCollectionView (IntPtr handle) : base(handle)
        {
            this.RegisterNibForCell (EditableTextCollectionCell.Nib, EditableTextCollectionCell.Key);
//          this.RegisterClassForCell (typeof(EditableTextCollectionCell), EditableTextCollectionCell.Key);
        }

        #region ICollectionCellProvider implementation

        public UICollectionViewCell GetCell (EditableText item, NSIndexPath indexPath)
        {
            var cell = (EditableTextCollectionCell)this.DequeueReusableCell (EditableTextCollectionCell.Key, indexPath);

            cell.Bind (item);

            return cell;
        }

        #endregion
    }
}

