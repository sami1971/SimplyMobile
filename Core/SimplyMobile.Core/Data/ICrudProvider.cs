using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public interface ICrudProvider : IDisposable
    {
        int Create<T>(T obj) where T : new();
        T Read<T>(object primaryKey) where T : new();
        IEnumerable<T> Read<T>() where T : new();
        int Update(object obj);
        int Delete<T>(object primaryKey) where T : new();
        int Delete(object objectToDelete);

        void RunInTransaction(Action action);
    }
}
