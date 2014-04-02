using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimplyMobile.Web.CanvasJs
{
    public class ColumnData
    {
        public string type { get { return "column"; } }

        public ObservableCollection<DataPoint> dataPoints { get; set; }
    }
}

