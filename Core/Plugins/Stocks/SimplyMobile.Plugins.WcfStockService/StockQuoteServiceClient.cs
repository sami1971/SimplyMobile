using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace SimplyMobile.Plugins.WcfStockService
{
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockQuoteServiceClient : ClientBase<IStockQuoteService>, IStockQuoteService
    {
        public static EndpointAddress DefaultEndpoint
        {
            get
            {
                return new EndpointAddress("http://www.restfulwebservices.net/wcf/StockQuoteService.svc");
            }
        }

        public static BasicHttpBinding DefaultBindings
        {
            get
            {
                var timeout = TimeSpan.FromMilliseconds(5000);
                return new BasicHttpBinding()
                {
                    Name = "basicHttpBinding",
                    MaxReceivedMessageSize = 67108864,
                    ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
                    {
                        MaxArrayLength = 2147483646,
                        MaxStringContentLength = 5242880,
                    },
                    SendTimeout = timeout,
                    OpenTimeout = timeout,
                    ReceiveTimeout = timeout
                };
            }
        }

        public StockQuoteServiceClient() : this(DefaultBindings, DefaultEndpoint)
        {
        }

        public StockQuoteServiceClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public StockQuote GetStockQuote(string request)
        {
            return base.Channel.GetStockQuote(request);
        }

        public System.Threading.Tasks.Task<StockQuote> GetStockQuoteAsync(string request)
        {
            return Task.Factory.StartNew(() => this.GetStockQuote(request));
        }

        public StockQuote[] GetWorldMajorIndices()
        {
            return base.Channel.GetWorldMajorIndices();
        }

        public System.Threading.Tasks.Task<StockQuote[]> GetWorldMajorIndicesAsync()
        {
            return Task.Factory.StartNew((Func<StockQuote[]>)this.GetWorldMajorIndices);
        }
    }

}
