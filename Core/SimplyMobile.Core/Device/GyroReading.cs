using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class GyroReading : Vector3d
    {
        public GyroReading() { }

        public GyroReading(double x, double y, double z) : this(DateTime.Now, x, y, z) 
        {
        }

        public GyroReading(DateTimeOffset timeStamp, double x, double y, double z)
            : base(x, y, z)
        {
            this.TimeStamp = timeStamp;
        }

        public DateTimeOffset TimeStamp { get; set; }
    }
}
