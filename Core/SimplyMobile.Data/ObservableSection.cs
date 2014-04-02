using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SimplyMobile.Data
{
    public class ObservableSection
    {
        public object Section { get; set; }

        public ObservableCollection<object> Values { get; private set; }

        public ObservableSection (object section, IEnumerable<object> values)
        {
            this.Section = section;
            this.Values = new ObservableCollection<object> (values);
        }
    }
}

