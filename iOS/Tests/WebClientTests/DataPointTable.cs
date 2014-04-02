using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using WebClientTests;

namespace CanvasDemo.iOS
{
    [Register("DataPointTable")]
    public class DataPointTable : UITableView, ITableCellProvider<DataPoint>
    {
        public DataPointTable(IntPtr handle)
            : base(handle)
        {
        }

        public UITableViewCell GetCell(DataPoint item)
        {
            var newCell = this.DequeueReusableCell(LabelCell.Key) as LabelCell ?? LabelCell.Create();

            newCell.Bind(item);

            return newCell;
        }

        public float GetHeightForRow(NSIndexPath indexPath, DataPoint item)
        {
            return WebClientTestsViewController.UserInterfaceIdiomIsPhone ? 48 : 80;
        }
    }
}