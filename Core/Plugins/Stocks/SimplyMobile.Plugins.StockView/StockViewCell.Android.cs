using System;
using Android.Widget;
using Android.Content;
using SimplyMobile.Plugins.WcfStockService;

namespace SimplyMobile.Plugins.StockView
{
    /// <summary>
    /// Stock view for Android.
    /// </summary>
    public sealed class StockViewCell : LinearLayout
    {
        private TextView textName;
        private TextView textLast;
        private ToggleButton toggleUpdate;
        private StockQuote stockQuote;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplyMobile.Plugins.StockView.Android.StockView"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public StockViewCell (Context context) : base(context)
        {
            this.Orientation = Orientation.Vertical;

            textName = new TextView (this.Context);
            this.AddView (textName);

            textLast = new TextView (this.Context);
            this.AddView (textLast);

            this.toggleUpdate = new ToggleButton (this.Context) 
            {
                Text = "Click to update",
                TextOff = "Click to update",
                TextOn = "Updating..."
            };

            this.AddView (this.toggleUpdate);

            this.toggleUpdate.Click += async (sender, e) => 
            {
                if (this.toggleUpdate.Checked)
                {
                    await StockViewModel.StockModel.RefreshOrAdd(this.stockQuote.Symbol);
                    this.toggleUpdate.Checked = false;
                }
            };
        }

        /// <summary>
        /// Bind the specified stock.
        /// </summary>
        /// <param name="stock">Stock.</param>
        public void Bind(StockQuote stock)
        {
            this.stockQuote = stock;
            this.textName.Text = stock.Name;
            this.textLast.Text = stock.Last;
        }
    }
}

