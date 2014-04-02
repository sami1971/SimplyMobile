using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JavaScriptValidator
{
    public abstract class DocumentNode : INotifyPropertyChanged
    {
        bool enabled;
        public bool Enabled 
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        string name = string.Empty;
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        string caption = string.Empty;
        public string Caption 
        {
            get
            {
                return caption;
            }
            set
            {
                if (caption != value)
                {
                    caption = value;
                    this.NotifyPropertyChanged();
                }
            }
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

