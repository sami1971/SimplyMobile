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
using SimplyMobile.Core;
using Android.Util;

namespace SimplyMobile.Logging
{
    public class LogService : ILogService
    {
        public void Exception(object sender, Exception ex)
        {
            Log.Error(sender.ToString(), ex.Message);
        }

        public void Info(object sender, string message, params object[] parameters)
        {
            Log.Info(sender.ToString(), string.Format(message, parameters));
        }

        public void Warning(object sender, string warning, params object[] parameters)
        {
            Log.Warn(sender.ToString(), string.Format(warning, parameters));
        }
    }
}