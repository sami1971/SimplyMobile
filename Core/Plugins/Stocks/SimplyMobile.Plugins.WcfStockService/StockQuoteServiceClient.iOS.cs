using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SimplyMobile.Plugins.WcfStockService
{
    /// <summary>
    /// Stock quote service client in iOS needs to override the dynamic channel creation.
    /// </summary>
    public partial class StockQuoteServiceClient
    {
        /// <summary>
        /// Creates the channel.
        /// </summary>
        /// <returns>The channel.</returns>
        protected override IStockQuoteService CreateChannel ()
        {
            return new StockQuoteChannel(this);
        }

        private class StockQuoteChannel : ChannelBase<IStockQuoteService>, IStockQuoteService
        {
            public StockQuoteChannel(ClientBase<IStockQuoteService> client) :
                base(client)
            {
            }

            public StockQuote GetStockQuote (string request)
            {
                return (StockQuote)base.Invoke("GetStockQuote", new object[] { request });
            }

            public Task<StockQuote> GetStockQuoteAsync (string request)
            {
                return Task.Factory.StartNew (() => { return GetStockQuote(request); } );
            }

            public StockQuote[] GetWorldMajorIndices ()
            {
                return (StockQuote[])base.Invoke("GetWorldMajorIndices", null);
            }

            public Task<StockQuote[]> GetWorldMajorIndicesAsync ()
            {
                return Task.Factory.StartNew ((Func<StockQuote[]>)GetWorldMajorIndices);
            }
        }
    }
}

