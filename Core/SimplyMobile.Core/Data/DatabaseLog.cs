using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class DatabaseLog : ILogService
    {
        private ICrudProvider db;

        public DatabaseLog(ICrudProvider db)
        {
            this.db = db;
        }

        public void Exception(object sender, Exception ex)
        {
            ex.TryToStore(db, sender);
        }

        public void Info(object sender, string message, params object[] parameters)
        {
            try
            {
                db.Create(new InfoEntry(sender, message, parameters));
            }
            catch (Exception ex)
            {
                ex.TryToStore(db, this);
            }
        }

        public void Warning(object sender, string warning, params object[] parameters)
        {
            try
            {
                db.Create(new WarningEntry(sender, warning, parameters));
            }
            catch (Exception ex)
            {
                ex.TryToStore(db, this);
            }
        }
    }
}
