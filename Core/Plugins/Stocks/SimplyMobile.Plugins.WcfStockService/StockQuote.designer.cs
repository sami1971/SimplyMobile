using System.Runtime.Serialization;

namespace SimplyMobile.Plugins.WcfStockService
{
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [DataContractAttribute(Name = "StockQuote", Namespace = "http://www.restfulwebservices.net/DataContracts/2008/01")]
    public partial class StockQuote : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string SymbolField;

        private string LastField;

        private string DateField;

        private string TimeField;

        private string ChangeField;

        private string OpenField;

        private string HighField;

        private string LowField;

        private string VolumeField;

        private string MktCapField;

        private string PreviousCloseField;

        private string PercentageChangeField;

        private string AnnRangeField;

        private string EarnsField;

        private string PEField;

        private string NameField;

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

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Symbol
        {
            get
            {
                return this.SymbolField;
            }
            set
            {
                this.SymbolField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public string Last
        {
            get
            {
                return this.LastField;
            }
            set
            {
                this.LastField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Date
        {
            get
            {
                return this.DateField;
            }
            set
            {
                this.DateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string Time
        {
            get
            {
                return this.TimeField;
            }
            set
            {
                this.TimeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string Change
        {
            get
            {
                return this.ChangeField;
            }
            set
            {
                this.ChangeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Open
        {
            get
            {
                return this.OpenField;
            }
            set
            {
                this.OpenField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string High
        {
            get
            {
                return this.HighField;
            }
            set
            {
                this.HighField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string Low
        {
            get
            {
                return this.LowField;
            }
            set
            {
                this.LowField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string Volume
        {
            get
            {
                return this.VolumeField;
            }
            set
            {
                this.VolumeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string MktCap
        {
            get
            {
                return this.MktCapField;
            }
            set
            {
                this.MktCapField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string PreviousClose
        {
            get
            {
                return this.PreviousCloseField;
            }
            set
            {
                this.PreviousCloseField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string PercentageChange
        {
            get
            {
                return this.PercentageChangeField;
            }
            set
            {
                this.PercentageChangeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public string AnnRange
        {
            get
            {
                return this.AnnRangeField;
            }
            set
            {
                this.AnnRangeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string Earns
        {
            get
            {
                return this.EarnsField;
            }
            set
            {
                this.EarnsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public string PE
        {
            get
            {
                return this.PEField;
            }
            set
            {
                this.PEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }

}
