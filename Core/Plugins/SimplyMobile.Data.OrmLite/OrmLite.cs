using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class OrmLite : ICrudProvider, IDisposable
    {
        private readonly IDbConnection connection;

        public OrmLite(string path) : this(path, SqliteDialect.Provider) { }

        public OrmLite(string path, IOrmLiteDialectProvider dialectProvider)
        {
            OrmLiteConfig.DialectProvider = dialectProvider;
            this.connection = path.OpenDbConnection();
        }

        public int Create<T>(T obj) where T : new()
        {
            this.connection.CreateTable<T>();
            this.connection.Insert(obj);
            return 1;
        }

        public T Read<T>(object primaryKey) where T : new()
        {
            return this.connection.Id<T>(primaryKey);
        }

        public IEnumerable<T> Read<T>() where T : new()
        {
            return this.connection.Select<T>();
        }

        public int Update(object obj)
        {
            this.connection.Update(obj);
            return 1;
        }

        public int Delete<T>(object primaryKey) where T : new()
        {
            this.connection.DeleteById<T>(primaryKey);
            return 1;
        }

        public int Delete(object objectToDelete)
        {
            this.connection.Delete(objectToDelete);
            return 1;
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
