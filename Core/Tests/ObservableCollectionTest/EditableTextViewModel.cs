using System;
using SimplyMobile.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableCollectionTest
{
    public class EditableTextViewModel : INotifyPropertyChanged
    {
        private EditableText latestTextChange;
        private EditableText latestCheckChange;

        private static EditableTextViewModel instance;

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public EditableText LatestTextChange
        {
            get 
            { 
                return latestTextChange; 
            }
            private set 
            {
                this.latestTextChange = value;
                this.NotifyPropertyChanged ();
            }
        }

        public EditableText LatestCheckChange
        {
            get 
            { 
                return latestCheckChange; 
            }
            private set 
            {
                this.latestCheckChange = value;
                this.NotifyPropertyChanged ();
            }
        }

        public ObservableDataSource<EditableText> Items { get; private set; }

        public EditableTextViewModel ()
        {
            this.Items = new ObservableDataSource<EditableText> ();
            this.latestTextChange = new EditableText ();
            this.latestCheckChange = new EditableText ();
        }

        public static EditableTextViewModel Instance 
        {
            get { return instance ?? (instance = new EditableTextViewModel ()); }
        }

        public void AddItem(EditableText item)
        {
            this.Items.Add (item);
            item.PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            var editableText = sender as EditableText;

            if (editableText == null)
            {
                return;
            }

            if (e.PropertyName == "Text")
            {
                this.LatestTextChange = editableText;
                if (editableText == this.LatestCheckChange)
                {
                    this.LatestCheckChange = editableText;
                }
            }
            else
            {
                this.LatestCheckChange = editableText;
            }
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

