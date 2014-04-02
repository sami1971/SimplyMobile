using System;
using NUnit.Framework;
using System.IO;
using ServiceStack.DataAnnotations;
using System.Diagnostics;

using ServiceStack.OrmLite;
using System.Linq;
using SQLite.Net.Interop;

namespace SQLiteSpeedTests
{
    public class Course
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int ID{ get; set; }
        public string Ident{ get; set; }
        public string Description{ get; set; }
        public string State{ get; set; }
        public string Lecturer{ get; set; }

        public Course()
        {
        }
    }

    public class CourseOrmLite
    {
        [ServiceStack.DataAnnotations.PrimaryKey, AutoIncrement]
        public int ID{ get; set; }
        public string Ident{ get; set; }
        public string Description{ get; set; }
        public string State{ get; set; }
        public string Lecturer{ get; set; }
    }

    public class CourseNoAutoIncrement
    {
        [SQLite.Net.Attributes.PrimaryKey]
        public int ID{ get; set; }
        public string Ident{ get; set; }
        public string Description{ get; set; }
        public string State{ get; set; }
        public string Lecturer{ get; set; }

    }

    public class CourseOrmLiteNoAutoIncrement
    {
        [ServiceStack.DataAnnotations.PrimaryKey, AutoIncrement]
        public int ID{ get; set; }
        public string Ident{ get; set; }
        public string Description{ get; set; }
        public string State{ get; set; }
        public string Lecturer{ get; set; }
    }

    [TestFixture]
    public class TestsSample
    {
        private const int Count = 1000;

        private readonly ISQLitePlatform Platform = 
#if __IOS__
            new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS ();
#elif __ANDROID__
        SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid ();
#elif WINDOWS_PHONE

#endif

        readonly string dbPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
            "test.db");

        [SetUp]
        public void Setup()
        {
            if (File.Exists(dbPath))
                File.Delete(dbPath);
        }

        [TearDown]
        public void Tear()
        {
            if (File.Exists(dbPath))
                File.Delete(dbPath);
        }

        [Test]
        public void Sanity()
        {
            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.NoMutex | SQLiteOpenFlags.ProtectionNone))
            {
                var items = Enumerable.Range(0, Count).Select(a=>new Course () {
                    Description = "It seems to be very slow",
                    State = "public",
                    Lecturer = "Kevin Tough"
                }).ToList();

                connection.CreateTable<Course> ();

                connection.BeginTransaction ();

                connection.InsertAll (items);
                connection.Commit ();
            }
        }

//        [Test]
        public void SQLiteCommand()
        {
            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<Course> ();

                var cmd = connection.CreateCommand ("Insert Into course (Description, State, Lecturer) Values(@p2, @p3, @p4)");

                var watch = Stopwatch.StartNew ();
                connection.BeginTransaction ();
                for (int i = 0; i < Count; i++)
                {
                    cmd.Bind ("@p2", "It seems to be very slow");
                    cmd.Bind ("@p3", "public");
                    cmd.Bind ("@p4", "Kevin Tough");
                    cmd.ExecuteNonQuery ();
                }

                connection.Commit ();

                watch.Stop ();

                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL Command");
            }
        }

        [Test]
        public void SQLiteNetInsert()
        {
            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<Course> ();

                var watch = Stopwatch.StartNew ();
                connection.BeginTransaction ();
                for (int i = 0; i < Count; i++)
                {
                    connection.Insert (new Course () {
                        Description = "It seems to be very slow",
                        State = "public",
                        Lecturer = "Kevin Tough"
                    });
                }

                connection.Commit ();

                watch.Stop ();
                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL Insert");
            }
        }

        [Test]
        public void SQLiteNetInsertAll()
        {
            var items = Enumerable.Range(0, Count).Select(a=>new Course () {
                Description = "It seems to be very slow",
                State = "public",
                Lecturer = "Kevin Tough"
            }).ToList();

            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<Course> ();

                var watch = Stopwatch.StartNew ();
                connection.BeginTransaction ();

                connection.InsertAll (items);

                connection.Commit ();

                watch.Stop ();
                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL InsertAll");
            }
        }

        [Test]
        public void SQLiteNetInsertAllNoAutoIncrement()
        {
            var items = Enumerable.Range(0, Count).Select(a=>new CourseNoAutoIncrement () {
                ID = a,
                Description = "It seems to be very slow",
                State = "public",
                Lecturer = "Kevin Tough"
            }).ToList();

            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<CourseNoAutoIncrement> ();

                var watch = Stopwatch.StartNew ();
                connection.BeginTransaction ();

                connection.InsertAll (items);

                connection.Commit ();

                watch.Stop ();
                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL InsertAll No Auto");
            }
        }

        [Test]
        public void SQLiteNetInsertSync()
        {
            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
//                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<Course> ();

                var watch = Stopwatch.StartNew ();
                connection.BeginTransaction ();
                for (int i = 0; i < Count; i++)
                {
                    connection.Insert (new Course () {
                        Description = "It seems to be very slow",
                        State = "public",
                        Lecturer = "Kevin Tough"
                    });
                }

                connection.Commit ();

                watch.Stop ();
                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL Insert");
            }
        }

        [Test]
        public void SQLiteNetInsertAllSync()
        {
            var items = Enumerable.Range(0, Count).Select(a=>new Course () {
                Description = "It seems to be very slow",
                State = "public",
                Lecturer = "Kevin Tough"
            }).ToList();

            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
//                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<Course> ();

                var watch = Stopwatch.StartNew ();
                connection.BeginTransaction ();

                connection.InsertAll (items);

                connection.Commit ();

                watch.Stop ();
                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL InsertAll");
            }
        }

        [Test]
        public void SQLiteNetInsertAllNoAutoIncrementSync()
        {


            using (var connection = new SQLite.Net.SQLiteConnection (Platform, dbPath, SQLite.Net.Interop.SQLiteOpenFlags.Create | SQLite.Net.Interop.SQLiteOpenFlags.ReadWrite | SQLite.Net.Interop.SQLiteOpenFlags.SharedCache | SQLite.Net.Interop.SQLiteOpenFlags.NoMutex | SQLite.Net.Interop.SQLiteOpenFlags.ProtectionNone))
            {
//                connection.Execute ("PRAGMA synchronous = OFF");
                connection.CreateTable<CourseNoAutoIncrement> ();

                var watch = Stopwatch.StartNew ();

                var items = Enumerable.Range(0, Count).Select(a=>new CourseNoAutoIncrement () {
                    ID = a,
                    Description = "It seems to be very slow",
                    State = "public",
                    Lecturer = "Kevin Tough"
                }).ToList();

                connection.BeginTransaction ();

                connection.InsertAll (items);

                connection.Commit ();

                watch.Stop ();
                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "SQL InsertAll No Auto");
            }
        }

        [Test]
        public void OrmLiteInsert()
        {
            OrmLiteConfig.DialectProvider = SqliteDialect.Provider;

            using (var connection = dbPath.OpenDbConnection ())
            {
                connection.CreateTable<CourseOrmLite> ();

                var watch = Stopwatch.StartNew ();
                var t = connection.BeginTransaction ();
                for (int i = 0; i < Count; i++)
                {
                    connection.Insert (new CourseOrmLite () {
                        Description = "It seems to be very slow",
                        State = "public",
                        Lecturer = "Kevin Tough"
                    });
                }

                t.Commit ();

                watch.Stop ();

                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "OrmLite Insert");
            }
        }

        [Test]
        public void OrmLiteInsertAll()
        {
            OrmLiteConfig.DialectProvider = SqliteDialect.Provider;

            var items = Enumerable.Range(0, Count).Select(a=>new CourseOrmLite () {
                Description = "It seems to be very slow",
                State = "public",
                Lecturer = "Kevin Tough"
            }).ToList();
                    
            using (var connection = dbPath.OpenDbConnection ())
            {
                connection.CreateTable<CourseOrmLite> ();

                var watch = Stopwatch.StartNew ();

                var t = connection.BeginTransaction ();
                connection.InsertAll (items);

                t.Commit ();

                watch.Stop ();

                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "OrmLite InsertAll");
            }
        }

        [Test]
        public void OrmLiteInsertAllNoAutoIncrement()
        {
            OrmLiteConfig.DialectProvider = SqliteDialect.Provider;

            var items = Enumerable.Range(0, Count).Select(a=>new CourseOrmLiteNoAutoIncrement () {
                ID = a,
                Description = "It seems to be very slow",
                State = "public",
                Lecturer = "Kevin Tough"
            }).ToList();

            using (var connection = dbPath.OpenDbConnection ())
            {
                connection.CreateTable<CourseOrmLiteNoAutoIncrement> ();

                var watch = Stopwatch.StartNew ();

                var t = connection.BeginTransaction ();
                connection.InsertAll (items);

                t.Commit ();

                watch.Stop ();

                Console.WriteLine ("{1} {0}ms", watch.ElapsedMilliseconds, "OrmLite InsertAll No Auto");
            }
        }
    }
}

