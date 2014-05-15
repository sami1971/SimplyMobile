using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class OrmLite : ICrudProvider
    {
        private readonly IDbConnection connection;

        public OrmLite(IDbConnection connection, IOrmLiteDialectProvider dialectProvider)
        {
            OrmLiteConfig.DialectProvider = dialectProvider;
            this.connection = connection;

            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();
            }
        }

        private OrmLite(string path, IOrmLiteDialectProvider dialectProvider)
        {
            OrmLiteConfig.DialectProvider = dialectProvider;
            this.connection = path.OpenDbConnection();
        }

        public int Create<T>(T obj) where T : new()
        {
            this.connection.CreateTableIfNotExists<T>();
            this.connection.Insert(obj);
            return 1;
        }

        public T Read<T>(object primaryKey) where T : new()
        {
            this.connection.CreateTableIfNotExists<T>();
            return this.connection.Id<T>(primaryKey);
        }

        public IEnumerable<T> Read<T>() where T : new()
        {
            this.connection.CreateTableIfNotExists<T>();
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

        public int DeleteAll<T>()
        {
            var count = this.connection.Count<T> ();
            this.connection.DeleteAll<T> ();
            return (int)(count - this.connection.Count<T>());
        }

        public void Dispose()
        {
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }

            this.connection.Dispose();
        }


        public void RunInTransaction(Action action)
        {
            IDbTransaction transaction = null;

            try
            {
                transaction = this.connection.BeginTransaction();
                action();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}
