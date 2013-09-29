using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Plugins.StockView;

namespace StockQuote
{
	[Activity (Label = "StockQuote", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private EditText textSymbol;
        private TextView textCurrent;
	    private Button buttonGet;
	    private ListView listStocks;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			this.textSymbol = FindViewById<EditText>(Resource.Id.textStockSymbol);
		    this.textCurrent = FindViewById<TextView>(Resource.Id.textCurrent);
		    this.buttonGet = FindViewById<Button>(Resource.Id.buttonGetQuote);
		    this.listStocks = FindViewById<ListView>(Resource.Id.listViewStocks);

            StockViewModel.StockModel.StockQuotes.Bind(this.listStocks);

			this.buttonGet.Click += async (sender, e) =>
			{
				var quote = await StockViewModel.StockModel.RefreshOrAdd (this.textSymbol.Text);
				if (quote != null)
				{
					this.textCurrent.Text = quote.Last;
				}
			};

		}
	}
}


