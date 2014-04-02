using System;

namespace JavaScriptValidator
{
    public class TextNode : DocumentNode
    {
        string text = string.Empty;
        public string Text {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    this.NotifyPropertyChanged ();
                }
            }
        }

        public TextNode ()
        {
        }
    }
}

