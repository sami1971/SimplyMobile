using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SimplyMobile.Data;

namespace JavaScriptValidator
{
    public class Document : INotifyPropertyChanged
    {
        public List<string> Validators { get; set; }

        public ObservableDataSource<DocumentNode> Nodes { get; set; }

        bool isValid;
        public bool IsValid 
        {
            get
            {
                return isValid;
            }
            set
            {
                if (isValid != value)
                {
                    isValid = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public Document()
        {
            Validators = new List<string> ();
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

