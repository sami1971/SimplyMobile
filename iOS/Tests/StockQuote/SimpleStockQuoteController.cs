using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.Plugins.StockView;
using SimplyMobile.Plugins.WcfStockService;
using System.Linq;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;

namespace StockQuote
{
    public partial class SimpleStockQuoteController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public SimpleStockQuoteController ()
            : base (UserInterfaceIdiomIsPhone ? "SimpleStockQuoteController_iPhone" : "SimpleStockQuoteController_iPad", null)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            // Perform any additional setup after loading the view, typically from a nib.

            SetControls();

            if (UserInterfaceIdiomIsPhone)
            {
                StockViewModel.StockModel.StockQuotes.Bind(this.tableViewStocks_iPhone);
//              this.tableViewStocks_iPhone.Delegate = new StockTableDelegate ();
            }
            else
            {
                StockViewModel.StockModel.StockQuotes.Bind(this.tableViewStocks);
//              this.tableViewStocks.Delegate = new StockTableDelegate ();
            }

            this.textStockSymbol.EditingChanged += (sender, e) => SetControls();

            this.buttonGetQuote.TouchUpInside += async (object sender, EventArgs e) => 
            {
                // close keyboard by resigning the text view responder
                this.textStockSymbol.ResignFirstResponder();
                // start activity indicator and disable update control
                this.activityIndicator.Hidden = false;
                this.activityIndicator.StartAnimating();
                this.buttonGetQuote.Enabled = false;

                this.textStockSymbol.Text = this.textStockSymbol.Text.ToUpper();

                // get stock quote asyncronously
                var quote = await StockViewModel.StockModel.RefreshOrAdd(this.textStockSymbol.Text);

                // async call has finished, lets update the last stock price
                // NOTE: table view will update automatically through the view model
                this.textStockPrice.Text = string.Format("${0}", quote.Last);
                // stop the activity indicator and enable update control
                this.activityIndicator.StopAnimating();
                this.buttonGetQuote.Enabled = true;
            };
        }

        private void SetControls()
        {
            this.buttonGetQuote.Enabled = !string.IsNullOrEmpty(this.textStockSymbol.Text);
        }
    }
}

