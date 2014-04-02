using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace StockQuote
{
    [Register("NavigationController")]
    public class NavigationController : UITabBarController
    {
        SimpleStockQuoteController stockQuoteController;
        StockTableViewController stockTableView;

        public NavigationController ()
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.stockQuoteController = new SimpleStockQuoteController () 
            {
                TabBarItem = new UITabBarItem (UITabBarSystemItem.Search, 0)
            };

            this.stockTableView = new StockTableViewController () 
            {
                TabBarItem = new UITabBarItem(UITabBarSystemItem.MostViewed, 1)
            };

            this.ViewControllers = new UIViewController[] 
            {
                this.stockQuoteController,
                this.stockTableView
            };

            this.SelectedViewController = this.stockQuoteController;


        }


    }
}

