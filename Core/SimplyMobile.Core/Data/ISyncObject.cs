using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public interface ISyncObject : IEquatable<ISyncObject>
    {
        DateTimeOffset Created { get; set; }

        DateTimeOffset Modified { get; set; }

        DateTimeOffset? Deleted { get; set; }
    }
}
