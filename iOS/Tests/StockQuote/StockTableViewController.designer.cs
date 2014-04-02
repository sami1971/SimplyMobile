// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace StockQuote
{
    [Register ("StockTableViewController")]
    partial class StockTableViewController
    {
        [Outlet]
        MonoTouch.UIKit.UITableView tableView { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }
        }
    }
}
