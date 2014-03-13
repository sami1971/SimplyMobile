using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Android.Util;
using System.Diagnostics;
using Android.Net;

namespace SimplyMobile.Web
{
    public class Reachability : IReachability
    {
        public async Task<bool> IsHostReachable(string host, TimeSpan timeout)
        {
            bool reachable = false;

            try
            {
                var stopwatch = Stopwatch.StartNew();
                Java.Lang.Process process = null;

                if ((long)Build.VERSION.SdkInt <= 16)
                {
                    // shiny APIS 
                    process = Java.Lang.Runtime.GetRuntime().Exec(string.Format("/system/bin/ping -w 1 -c 1 {0}", host));
                }
                else
                {
                    process = new Java.Lang.ProcessBuilder().
                        Command("/system/bin/ping", host).RedirectErrorStream(true).Start();
                }

                using (var reader = new StreamReader(process.InputStream))
                {
                    do
                    {
                        var result = await reader.ReadLineAsync();
                        Log.Info(this.ToString(), result);
                    } while (!reachable && stopwatch.ElapsedMilliseconds < timeout.TotalMilliseconds); 
                }

                process.Destroy();
                reachable = true;
            }
            catch (IOException e)
            {
                Log.Error(this.ToString(), e.Message);
                reachable = false;
            }

            return reachable;
        }

        public bool HasInternetConnection
        {
            get
            {
                var networkInfo = this.GetConnectivityManager().ActiveNetworkInfo;
                return networkInfo != null && networkInfo.IsConnectedOrConnecting;
            }
        }

        public async Task Ping(string host, CancellationToken token, IProgress<string> progress, int count = 0)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                Java.Lang.Process process = null;

                if ((long)Build.VERSION.SdkInt <= 16)
                {
                    // shiny APIS 
                    process = Java.Lang.Runtime.GetRuntime().Exec(string.Format("/system/bin/ping -w 1 -c 1 {0}", host));
                }
                else
                {
                    process = new Java.Lang.ProcessBuilder().
                        Command("/system/bin/ping", host).RedirectErrorStream(true).Start();
                }

                using (var reader = new StreamReader(process.InputStream))
                {
                    var n = 0;
                    do
                    {
                        var result = await reader.ReadLineAsync();
                        if (result != null)
                        {
                            Log.Info(this.ToString(), result);
                            progress.Report(result);
                        }
                        else
                        {
                            await Task.Delay(10);
                        }
                    } while (!token.IsCancellationRequested && (count <= 0 || n++ < count));
                }

                process.Destroy();
            }
            catch (IOException e)
            {
                Log.Error(this.ToString(), e.Message);
                progress.Report(e.Message);
            }
        }
    }
}