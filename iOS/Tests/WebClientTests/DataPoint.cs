using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebClientTests
{
    public class DataPoint : INotifyPropertyChanged
    {
        private string label;
        private long y;

        public string Label 
        {
            get { return this.label; }
            set
            {
                if (this.label != value)
                {
                    this.label = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public long Y 
        {
            get { return this.y; }
            set
            {
                if (this.y != value)
                {
                    this.y = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Label: {0}, Y: {1}", this.Label, this.Y);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

