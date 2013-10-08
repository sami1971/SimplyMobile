using System;
using Android.Widget;
using SimplyMobile.Data;
using Android.Content;
using Android.Views;
using SimplyMobile.Plugins.WcfStockService;

namespace SimplyMobile.Plugins.StockView
{
	/// <summary>
	/// Stock view table.
	/// </summary>
	public class StockViewTable : ListView, ITableCellProvider<StockQuote>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimplyMobile.Plugins.StockView.Android.StockViewTable"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public StockViewTable (Context context) : base(context)
		{
		}

		#region ITableCellProvider implementation
		/// <summary>
		/// Implements <see cref="SimplyMobile.Data.ITableCellProvider"/>.GetView function.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="item">Item for the view.</param>
		/// <param name="convertView">Convert view.</param>
		public View GetView (StockQuote stock, View convertView)
		{
			var view = convertView as StockViewCell ?? new StockViewCell(this.Context);

			view.Bind (stock);

			return view;
		}
		#endregion
	}
}

