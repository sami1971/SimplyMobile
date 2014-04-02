//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
using MonoTouch.UIKit;
using SimplyMobile.Data;
using MonoTouch.Foundation;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;


namespace StockQuote
{
    [Register("StockTableViewComplete")]
    public class StockTableViewComplete : UITableView, ITableCellProvider
    {
        private const string CellId = "StockTableViewComplete";

        public StockTableViewComplete ()
        {
        }

        public StockTableViewComplete (IntPtr handle) : base (handle)
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

