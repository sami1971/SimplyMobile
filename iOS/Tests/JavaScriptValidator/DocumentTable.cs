using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using SimplyMobile.Data;

namespace JavaScriptValidator
{
    [Register("DocumentTable")]
    public class DocumentTable : UITableView, ITableCellProvider<DocumentNode>
    {
        public DocumentTable ()
        {
        }

        public DocumentTable (IntPtr handle) : base(handle)
        {
        }


        #region ITableCellProvider implementation
        public UITableViewCell GetCell (DocumentNode item)
        {
            //UITableViewCell cell = null;
            if (item is CheckBoxNode)
            {
                var cell = this.DequeueReusableCell (CheckBoxCell.Key) as CheckBoxCell ?? CheckBoxCell.Create ();
                cell.Bind (item as CheckBoxNode);
                return cell;
            }
             
            if (item is TextNode)
            {
                var cell = this.DequeueReusableCell (TextCell.Key) as TextCell ?? TextCell.Create ();
                cell.Bind (item as TextNode);
                return cell;
            }

            return null;
        }
        public float GetHeightForRow (NSIndexPath indexPath)
        {
            return 80f;
        }
        #endregion
    }
}

