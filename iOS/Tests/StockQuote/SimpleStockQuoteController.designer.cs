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
    [Register ("SimpleStockQuoteController")]
    partial class SimpleStockQuoteController
    {
        [Outlet]
        MonoTouch.UIKit.UIActivityIndicatorView activityIndicator { get; set; }

        [Outlet]
        MonoTouch.UIKit.UIButton buttonGetQuote { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITableView tableViewStocks { get; set; }

        [Outlet]
        StockQuote.StockTableView_iPhone tableViewStocks_iPhone { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextField textStockPrice { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITextField textStockSymbol { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (activityIndicator != null) {
                activityIndicator.Dispose ();
                activityIndicator = null;
            }

            if (buttonGetQuote != null) {
                buttonGetQuote.Dispose ();
                buttonGetQuote = null;
            }

            if (tableViewStocks != null) {
                tableViewStocks.Dispose ();
                tableViewStocks = null;
            }

            if (textStockPrice != null) {
                textStockPrice.Dispose ();
                textStockPrice = null;
            }

            if (textStockSymbol != null) {
                textStockSymbol.Dispose ();
                textStockSymbol = null;
            }

            if (tableViewStocks_iPhone != null) {
                tableViewStocks_iPhone.Dispose ();
                tableViewStocks_iPhone = null;
            }
        }
    }
}
