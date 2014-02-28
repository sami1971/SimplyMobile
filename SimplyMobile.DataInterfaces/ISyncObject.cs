using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyMobile.DataInterfaces
{
    public interface ISyncObject : IEquatable<ISyncObject>
    {
        Guid Id { get; }
        DateTimeOffset Created { get; }
        DateTimeOffset Updated { get; }
        DateTimeOffset Deleted { get; }
    }
}
