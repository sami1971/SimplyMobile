using System;

namespace JavaScriptValidator
{
    public class CheckBoxNode : DocumentNode
    {
        bool @checked;
        public bool Checked 
        {
            get
            {
                return @checked;
            }
            set
            {
                if (@checked != value)
                {
                    @checked = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public CheckBoxNode ()
        {
        }
    }
}

