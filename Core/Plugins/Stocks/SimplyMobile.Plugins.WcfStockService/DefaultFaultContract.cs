namespace SimplyMobile.Plugins.WcfStockService
{
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DefaultFaultContract", Namespace = "http://GOTLServices.FaultContracts/2008/01")]
    public partial class DefaultFaultContract : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int ErrorIdField;

        private string ErrorMessageField;

        private System.Guid CorrelationIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public int ErrorId
        {
            get
            {
                return this.ErrorIdField;
            }
            set
            {
                this.ErrorIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string ErrorMessage
        {
            get
            {
                return this.ErrorMessageField;
            }
            set
            {
                this.ErrorMessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = 2)]
        public System.Guid CorrelationId
        {
            get
            {
                return this.CorrelationIdField;
            }
            set
            {
                this.CorrelationIdField = value;
            }
        }
    }

}
