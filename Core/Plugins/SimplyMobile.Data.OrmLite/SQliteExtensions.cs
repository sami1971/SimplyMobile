using System;
using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace SimplyMobile.Data.OrmLite
{
	public static class SQliteConnection
	{
		public static IDbConnection OpenConnection(this string fileName)
		{
			return fileName.ToDbConnection(SqliteOrmLiteDialectProvider.Instance);
		}
	}
}

