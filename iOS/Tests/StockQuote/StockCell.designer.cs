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
    [Register ("StockCell")]
    partial class StockCell
    {
        [Outlet]
        MonoTouch.UIKit.UIActivityIndicatorView activityUpdating { get; set; }

        [Outlet]
        MonoTouch.UIKit.UIImageView imageTrend { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelHighLow { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelLast { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelPercentageChange { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelStockName { get; set; }

        [Outlet]
        StockQuote.PlotView plotView { get; set; }

        [Outlet]
        MonoTouch.UIKit.UISwitch switchMonitor { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (plotView != null) {
                plotView.Dispose ();
                plotView = null;
            }

            if (activityUpdating != null) {
                activityUpdating.Dispose ();
                activityUpdating = null;
            }

            if (imageTrend != null) {
                imageTrend.Dispose ();
                imageTrend = null;
            }

            if (labelHighLow != null) {
                labelHighLow.Dispose ();
                labelHighLow = null;
            }

            if (labelLast != null) {
                labelLast.Dispose ();
                labelLast = null;
            }

            if (labelPercentageChange != null) {
                labelPercentageChange.Dispose ();
                labelPercentageChange = null;
            }

            if (labelStockName != null) {
                labelStockName.Dispose ();
                labelStockName = null;
            }

            if (switchMonitor != null) {
                switchMonitor.Dispose ();
                switchMonitor = null;
            }
        }
    }
}
