using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

namespace ObservableCollectionTest
{
    [Register("EditableTextTable")]
    public class EditableTextTable : UITableView, ITableCellProvider<EditableText>
    {
        public EditableTextTable ()
        {
        }

        public EditableTextTable (IntPtr handle) : base(handle)
        {
        }

        #region ITableCellProvider implementation

        public UITableViewCell GetCell (EditableText item)
        {
            var newCell = this.DequeueReusableCell(EditableTextTableCell.Key) 
                          as EditableTextTableCell ?? EditableTextTableCell.Create();

            newCell.Bind (item);

            return newCell;
        }

        public float GetHeightForRow(NSIndexPath indexPath, EditableText item)
        {
            return 67f;
        }

        #endregion
    }
}

