using System;
using System.Collections.Generic;
using System.Text;
using SimplyMobile.Core;

namespace PassingData
{
    public class SimpleViewModel : ViewModel
    {
        private string label = string.Empty;
        private string text = string.Empty;

        public string Label
        {
            get { return label; }
            set
            {
                if (label != value)
                {
                    label = value ?? string.Empty;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value ?? string.Empty;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
}
