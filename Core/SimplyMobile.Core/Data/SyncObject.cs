using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class SyncObject : ISyncObject
    {
        //public DateTime Created { get; set; }

        //public DateTime Modified { get; set; }

        //public DateTime? Deleted { get; set; }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }

        public DateTimeOffset? Deleted { get; set; }

        public bool Equals(ISyncObject other)
        {
            return this.Created.Equals(other.Created) &&
                this.Modified.Equals(other.Modified) &&
                this.Deleted.Equals(other.Deleted);
        }

        public override bool Equals(object obj)
        {
            return obj is ISyncObject ? this.Equals(obj) : false;
        }
    }
}
