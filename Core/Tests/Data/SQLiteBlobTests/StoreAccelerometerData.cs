using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimplyMobile.Device;
using SimplyMobile.Data;
using SimplyMobile.Core;

namespace SQLiteBlobTests
{
    public class AccelerometerData
    {
        public DateTimeOffset Time { get; set; }

        public AccelometerStatus Status { get; set; }

        public AccelerometerData()
        {
            Time = DateTime.Now;
        }

        public AccelerometerData(AccelometerStatus status)
            : this()
        {
            Status = status;
        }
    }

    public class StoreAccelerometerData : IMonitor
    {
        IAccelerometer meter;
        ICrudProvider crud;

        public StoreAccelerometerData(IAccelerometer accelerometer, ICrudProvider crud)
        {
            this.meter = accelerometer;
            this.crud = crud;
        }

        public event EventHandler<EventArgs<bool>> OnActiveChanged;

        public event EventHandler<EventArgs<Exception>> OnException;

        public bool Active
        {
            get { throw new NotImplementedException(); }
        }

        public bool Start()
        {
            try
            {
                this.meter.Interval = AccelerometerInterval.Fastest;
                this.meter.ReadingAvailable += meter_ReadingAvailable;
                return true;
            }
            catch (Exception ex)
            {
                ex.TryToStore(this.crud, this);
                return false;
            }
        }

        void meter_ReadingAvailable(object sender, EventArgs<AccelometerStatus> e)
        {
            try
            {
                this.crud.Create(new AccelerometerData(e.Value));
            }
            catch (Exception ex)
            {
                ex.TryToStore(this.crud, this);
            }
        }

        public void Stop()
        {
            this.meter.ReadingAvailable -= meter_ReadingAvailable;
        }
    }
}
