using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableCollectionTest
{
    public class EditableText : INotifyPropertyChanged
    {
        string text = string.Empty;
        bool @checked = false;

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.NotifyPropertyChanged ();
                }
            }
        }

        public bool Checked
        {
            get
            {
                return this.@checked;
            }
            set
            {
                if (this.@checked != value)
                {
                    this.@checked = value;
                    this.NotifyPropertyChanged ();
                }
            }
        }

        public override string ToString ()
        {
            return string.Format ("[Checked={1}, Text={0}]", Text, Checked);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

