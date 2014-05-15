using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class SQLiteAsync : SQLiteConnectionWithLock, ICrudProvider
    {
        public SQLiteAsync(ISQLitePlatform platform, SQLiteConnectionString connection)
            : base(platform, connection)
        {

        }

        public int Create<T>(T obj) where T : new()
        {
            this.CreateTable<T>();
            return this.Insert(obj);
        }

        public T Read<T>(object primaryKey) where T : new()
        {
            this.CreateTable<T>();
            return base.Find<T>(primaryKey);
        }

        public IEnumerable<T> Read<T>() where T : new()
        {
            this.CreateTable<T>();
            return base.Table<T>();
        }

        public new int Delete<T>(object primaryKey) where T : new()
        {
            return base.Delete<T>(primaryKey);
        }

        //public int DeleteAll<T>()
        //{
        //    return base.DeleteAll<T>();
        //}
    }
}
