using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
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
				AppDelegate.Data.Bind (this.tableViewStocks_iPhone);
				this.tableViewStocks_iPhone.Delegate = new StockTableDelegate ();
			}
			else
			{
				AppDelegate.Data.Bind (this.tableViewStocks);
				this.tableViewStocks.Delegate = new StockTableDelegate ();
			}

			this.textStockSymbol.EditingChanged += (sender, e) => SetControls();

			this.buttonGetQuote.TouchUpInside += async (object sender, EventArgs e) => 
			{
				this.buttonGetQuote.Enabled = false;

				this.textStockSymbol.ResignFirstResponder();
				this.activityIndicator.Hidden = false;
				this.activityIndicator.StartAnimating();

				try
				{
					this.textStockSymbol.Text = this.textStockSymbol.Text.ToUpper();

					var quote = await AppDelegate.StockClient.GetStockQuoteAsync(this.textStockSymbol.Text);
					this.textStockPrice.Text = string.Format("${0}", quote.Last);

					if (!AppDelegate.Data.Data.Cast<Stock>().Any(a=>a.Name.Equals(quote.Name)))
					{
						AppDelegate.Data.Data.Add(quote);
					}
				}
				catch (Exception ex)
				{
					this.textStockPrice.Text = ex.Message;
				}


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

