namespace SimplyMobile.Plugins.WcfStockService
{
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.restfulwebservices.net/ServiceContracts/2008/01", ConfigurationName = "IStockQuoteService")]
    public interface IStockQuoteService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "GetStockQuote", ReplyAction = "http://www.restfulwebservices.net/ServiceContracts/2008/01/IStockQuoteService/Get" +
            "StockQuoteResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DefaultFaultContract), Action = "http://www.restfulwebservices.net/ServiceContracts/2008/01/IStockQuoteService/Get" +
            "StockQuoteDefaultFaultContractFault", Name = "DefaultFaultContract", Namespace = "http://GOTLServices.FaultContracts/2008/01")]
        StockQuote GetStockQuote(string request);

        [System.ServiceModel.OperationContractAttribute(Action = "GetStockQuote", ReplyAction = "http://www.restfulwebservices.net/ServiceContracts/2008/01/IStockQuoteService/Get" +
            "StockQuoteResponse")]
        System.Threading.Tasks.Task<StockQuote> GetStockQuoteAsync(string request);

        [System.ServiceModel.OperationContractAttribute(Action = "GetWorldMajorIndices", ReplyAction = "http://www.restfulwebservices.net/ServiceContracts/2008/01/IStockQuoteService/Get" +
            "WorldMajorIndicesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DefaultFaultContract), Action = "http://www.restfulwebservices.net/ServiceContracts/2008/01/IStockQuoteService/Get" +
            "WorldMajorIndicesDefaultFaultContractFault", Name = "DefaultFaultContract", Namespace = "http://GOTLServices.FaultContracts/2008/01")]
        StockQuote[] GetWorldMajorIndices();

        [System.ServiceModel.OperationContractAttribute(Action = "GetWorldMajorIndices", ReplyAction = "http://www.restfulwebservices.net/ServiceContracts/2008/01/IStockQuoteService/Get" +
            "WorldMajorIndicesResponse")]
        System.Threading.Tasks.Task<StockQuote[]> GetWorldMajorIndicesAsync();
    }
}
