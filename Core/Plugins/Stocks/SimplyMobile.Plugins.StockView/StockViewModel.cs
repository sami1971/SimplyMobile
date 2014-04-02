// 
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using System;
using System.Linq;
using System.Threading.Tasks;
using SimplyMobile.Data;
using SimplyMobile.Plugins.WcfStockService;

using Stock = SimplyMobile.Plugins.WcfStockService.StockQuote;

namespace SimplyMobile.Plugins.StockView
{
    /// <summary>
    /// The stock view model.
    /// </summary>
    public class StockViewModel
    {
        /// <summary>
        /// The static model.
        /// </summary>
        private static StockViewModel staticModel;

        /// <summary>
        /// The stock quote service.
        /// </summary>
        private readonly IStockQuoteService stockQuoteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockViewModel"/> class.
        /// </summary>
        /// <param name="stockQuoteService">
        /// The stock quote service.
        /// </param>
        public StockViewModel(IStockQuoteService stockQuoteService)
        {
            this.stockQuoteService = stockQuoteService;
            this.StockQuotes = new ObservableDataSource<Stock>();
        }

        /// <summary>
        /// Gets a static view model instance
        /// </summary>
        public static StockViewModel StockModel
        {
            get { return staticModel ?? (staticModel = new StockViewModel(new StockQuoteServiceClient())); }
        }

        /// <summary>
        /// Gets the stock quotes.
        /// </summary>
        public ObservableDataSource<Stock> StockQuotes
        {
            get;
            private set;
        }

        /// <summary>
        /// Refresh or add a stock symbol.
        /// </summary>
        /// <param name="symbol">
        /// The symbol.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<StockQuote> RefreshOrAdd(string symbol)
        {
            var quote = new StockQuote();

            if (string.IsNullOrEmpty(symbol))
            {
                return quote;
            }

            try
            {
                quote = await this.stockQuoteService.GetStockQuoteAsync(symbol);
                var existingQuote = this.StockQuotes.Data.Cast<StockQuote>().FirstOrDefault(a => a.Symbol == symbol);

                if (existingQuote == null)
                {
                    this.StockQuotes.Add(quote);
                }
                else
                {
                    this.StockQuotes.Replace(existingQuote, quote);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine (ex.Message);
            }
            return quote;
        }
    }
}
