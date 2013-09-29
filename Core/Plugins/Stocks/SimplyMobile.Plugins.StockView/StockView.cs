using System;
using Android.Widget;
using Android.Content;
using SimplyMobile.Plugins.WcfStockService;

namespace SimplyMobile.Plugins.StockView.Android
{
	public sealed class StockView : LinearLayout
	{
		private TextView textName;
		private TextView textLast;
		private ToggleButton toggleUpdate;
		private StockQuote stockQuote;

		public StockView (Context context) : base(context)
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

		public void Bind(StockQuote stock)
		{
			this.stockQuote = stock;
			this.textName.Text = stock.Name;
			this.textLast.Text = stock.Last;
		}
	}
}

