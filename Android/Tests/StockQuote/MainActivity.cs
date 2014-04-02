using System;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using Android.OS;
using SimplyMobile.Plugins.StockView;

namespace StockQuote
{
    /// <summary>
    /// The main activity.
    /// </summary>
    [Activity (Label = "StockQuote", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private LinearLayout mainLayout;
        private EditText textSymbol;
        private TextView textCurrent;
        private Button buttonGet;
        private StockViewTable listStocks;

        /// <summary>
        /// The on create.
        /// </summary>
        /// <param name="bundle">
        /// The bundle.
        /// </param>
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            this.mainLayout = FindViewById<LinearLayout> (Resource.Id.mainLayout);
            this.textSymbol = FindViewById<EditText>(Resource.Id.textStockSymbol);
            this.textCurrent = FindViewById<TextView>(Resource.Id.textCurrent);
            this.buttonGet = FindViewById<Button>(Resource.Id.buttonGetQuote);
            this.listStocks = FindViewById<StockViewTable> (Resource.Id.listView1);
//          this.listStocks = new StockViewTable (this);
//
//          this.mainLayout.AddView (this.listStocks);

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


