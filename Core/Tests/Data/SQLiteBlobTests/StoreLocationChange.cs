using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteBlobTests
{
    public class StoreLocationChange
    {
        ILocationMonitor locationMonitor;
        ICrudProvider db;
        
        public StoreLocationChange(ILocationMonitor locationMonitor, ICrudProvider db)
        {
            this.locationMonitor = locationMonitor;
            this.db = db;
        }

        public bool Start()
        {
            try
            {
                this.locationMonitor.DesiredAccuracy = Accuracy.High;
                this.locationMonitor.LocationChanged += locationMonitor_LocationChanged;
                return true;
            }
            catch (Exception ex)
            {
                ex.TryToStore(this.db, this);
                return false;
            }
        }

        void locationMonitor_LocationChanged(object sender, Coordinates e)
        {
            try
            {
                this.db.Create(e);
            }
            catch (Exception ex)
            {
                ex.TryToStore(this.db, this);
            }
        }

        public void Stop()
        {
            this.locationMonitor.LocationChanged += locationMonitor_LocationChanged;
        }
    }
}
