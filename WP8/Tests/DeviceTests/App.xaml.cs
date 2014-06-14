//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DeviceTests.Resources;
using SimplyMobile.IoC;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WindowsPhone8;
using SimplyMobile.Device;
using SimplyMobile.Text;
using SQLite.Net;
using SimplyMobile.Data;
using SQLiteBlobTests;
using Windows.Storage;
using System.IO;
using SimplyMobile.Location;
using SimplyMobile.Core;

namespace DeviceTests
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            PrintDir(new DirectoryInfo(ApplicationData.Current.LocalFolder.Path));

            //var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var resolver = DependencyResolver.Current;
            resolver.RegisterService<ISQLitePlatform, SQLitePlatformWP8>();

            //resolver.RegisterService<IPhone, WindowsPhone>()
            //    .RegisterService<IBluetoothHub, BluetoothHub>();

            resolver.RegisterService<IDevice>(new WindowsPhoneDevice());

            DependencyResolver.Current.RegisterService<IAccelerometer, AccelerometerImpl>();
            DependencyResolver.Current.RegisterService<IBattery, BatteryImpl>();
            //DependencyResolver.Current.RegisterService<IJsonSerializer, SimplyMobile.Text.ServiceStack.JsonSerializer>();
            DependencyResolver.Current.RegisterService<IJsonSerializer, SimplyMobile.Text.JsonNet.JsonSerializer>();
            DependencyResolver.Current.RegisterService<IBlobSerializer>(t => t.GetService<IJsonSerializer>().AsBlobSerializer());

            DependencyResolver.Current.RegisterService<ICrudProvider>(t =>
                new SQLiteAsync(
                    t.GetService<ISQLitePlatform>(),
                    new SQLiteConnectionString(
                        Path.Combine(ApplicationData.Current.LocalFolder.Path, /*"Shared", "Media",*/ "device2.db"),
                        true,
                        t.GetService<IBlobSerializer>())
                    ));

            DependencyResolver.Current.RegisterService<ILogService>(t =>
                new DatabaseLog(new SQLiteAsync(t.GetService<ISQLitePlatform>(), new SQLiteConnectionString(
                    Path.Combine(ApplicationData.Current.LocalFolder.Path, "device.log"),
                    true,
                    t.GetService<IBlobSerializer>())
                    )));

            DependencyResolver.Current.RegisterService<StoreAccelerometerData>(
                new StoreAccelerometerData(
                    new AccelerometerImpl(),
                    DependencyResolver.Current.GetService<ICrudProvider>()));

            DependencyResolver.Current.RegisterService<StoreBatteryData>(
                new StoreBatteryData(
                    new BatteryImpl(),
                    DependencyResolver.Current.GetService<ICrudProvider>()));

            DependencyResolver.Current.RegisterService<StoreLocationChange>(
                new StoreLocationChange(
                    new LocationMonitorImpl(TimeSpan.FromMilliseconds(100)),
                    DependencyResolver.Current.GetService<ICrudProvider>()));

            //DependencyResolver.Current.GetService<StoreAccelerometerData>().Start();
            //DependencyResolver.Current.GetService<StoreBatteryData>().Start();
            //DependencyResolver.Current.GetService<StoreLocationChange>().Start();

            DependencyResolver.Current.GetService<ILogService>().Info(this, "Application started");
        }

        private static void PrintDir(DirectoryInfo di)
        {
            foreach (var fi in di.GetFiles())
            {
                System.Diagnostics.Debug.WriteLine(fi.FullName);
            }

            foreach (var dir in di.GetDirectories())
            {
                PrintDir(dir);
            }
        }
        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            DependencyResolver.Current.GetService<ILogService>().Info(this, "Application activated, args: {0}", e);
            //DependencyResolver.Current.GetService<StoreAccelerometerData>().Start();
            //DependencyResolver.Current.GetService<StoreBatteryData>().Start();
            //DependencyResolver.Current.GetService<StoreLocationChange>().Start();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            DependencyResolver.Current.GetService<ILogService>().Info(this, "Application deactivated, args: {0}", e);

            //DependencyResolver.Current.GetService<StoreAccelerometerData>().Stop();
            //DependencyResolver.Current.GetService<StoreBatteryData>().Stop();
            //DependencyResolver.Current.GetService<StoreLocationChange>().Stop();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            DependencyResolver.Current.GetService<ILogService>().Info(this, "Application closing, args: {0}", e);
            //try
            //{
            //    DependencyResolver.Current.GetService<StoreAccelerometerData>().Stop();
            //    DependencyResolver.Current.GetService<StoreBatteryData>().Stop();
            //    DependencyResolver.Current.GetService<StoreLocationChange>().Stop();
            //}
            //catch (Exception ex)
            //{
            //    DependencyResolver.Current.GetService<ILogService>().Exception(this, ex);
            //}
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            try
            {
                DependencyResolver.Current.GetService<ILogService>().Exception(sender, e.Exception);
            }
            catch
            {

            }
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                DependencyResolver.Current.GetService<ILogService>().Exception(sender, e.ExceptionObject);
            }
            catch
            {

            }
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                var flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}