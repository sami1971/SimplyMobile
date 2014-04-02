using System;

namespace SimplyMobile.Plugins.WcfStockService
{
    public partial class StockQuote
    {
        public override string ToString ()
        {
            return string.Format ("{0} {1}", this.Symbol, this.Last);
        }
    }
}

