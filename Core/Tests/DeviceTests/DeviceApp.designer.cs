using System;
using System.Collections.ObjectModel;
using SimplyMobile.Core;
using SimplyMobile.Data;
using SimplyMobile.Device;
using SimplyMobile.IoC;
using System.IO;
using SQLite.Net;
using SQLite.Net.Interop;
using SimplyMobile.Text;
using SQLiteBlobTests;
using SimplyMobile.Location;

namespace DeviceTests
{
    public partial class DeviceApp : MobileApp
    {
        private const string dbFile = "events.db";

        /// <summary>
        /// The battery status.
        /// </summary>
        private static ObservableDataSource<BatteryStatus> batteryStatus;

        /// <summary>
        /// Gets the battery status.
        /// </summary>
        public static ObservableDataSource<BatteryStatus> BatteryStatus
        {
            get
            {
                return batteryStatus ?? (batteryStatus = new ObservableDataSource<BatteryStatus>());
            }
        }

        /// <summary>
        /// Call this when the application has finished loading.
        /// </summary>
        private void OnStart()
        {
            DependencyResolver.Current.RegisterService<ILocationMonitor, LocationMonitorImpl>();
            DependencyResolver.Current.RegisterService<IAccelerometer, AccelerometerImpl>();
            DependencyResolver.Current.RegisterService<IBattery, BatteryImpl>();
            DependencyResolver.Current.RegisterService<IJsonSerializer, SimplyMobile.Text.ServiceStack.JsonSerializer>();
            DependencyResolver.Current.RegisterService<IBlobSerializer>(t=> t.GetService<IJsonSerializer>().AsBlobSerializer());

#if USE_ORMLITE
            DependencyResolver.Current.RegisterService<ICrudProvider>(new OrmLite(Path.Combine(GetPath(), dbFile)));
#else
            DependencyResolver.Current.RegisterService<ICrudProvider>(t =>
                new SQLiteAsync(
                    t.GetService<ISQLitePlatform>(),
                    new SQLiteConnectionString(
                        Path.Combine(GetPath(), dbFile), 
                        true, 
                        t.GetService<IBlobSerializer>())
                    ));
#endif
            DependencyResolver.Current.RegisterService<StoreAccelerometerData>(
                new StoreAccelerometerData(
                    new AccelerometerImpl(), 
                    DependencyResolver.Current.GetService<ICrudProvider>()));
            //Battery.OnChargerStatusChanged += (s, a) => BatteryStatus.Data.Add(Battery.Status);
            //Battery.OnLevelChange += (s, a) => BatteryStatus.Data.Add(Battery.Status);

        }

		private void OnStop()
		{

		}

        internal static string GetPath()
        {
            //Environment.SpecialFolder.ApplicationData
#if WINDOWS_PHONE
			var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shared" + Path.DirectorySeparatorChar + "Media");
#elif __ANDROID__
            var path = "/sdcard/";
#else
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#endif
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}