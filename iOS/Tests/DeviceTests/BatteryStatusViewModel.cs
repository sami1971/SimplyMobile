using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DeviceTests
{
    public class BatteryStatusViewModel
    {
        public ObservableCollection<BatteryStatusViewModel> BatteryStatus
        {
            get; 
            set;
        }
    } 
}
