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
using Android.Util;
using Java.Lang;
using Java.Lang.Reflect;

namespace SimplyMobile.Runtime
{
    public static class RuntimeType
    {
        private static readonly string SELECT_RUNTIME_PROPERTY = "persist.sys.dalvik.vm.lib";
        private static readonly string LIB_DALVIK = "libdvm.so";
        private static readonly string LIB_ART = "libart.so";
        private static readonly string LIB_ART_D = "libartd.so";

        public static AndroidRuntimeType Current
        {
            get
            {
                try
                {
                    var systemProperties = Java.Lang.Class.ForName("android.os.SystemProperties");

                    try
                    {
                        var str = new Java.Lang.String();
                        var getMethod = systemProperties.GetMethod("get", str.Class, str.Class);

                        if (getMethod == null)
                        {
                            Log.Info("RuntimeType", "Unable to get method for System Properties");
                            return AndroidRuntimeType.Unknown;
                        }

                        try
                        {
                            var value = getMethod.Invoke(systemProperties, SELECT_RUNTIME_PROPERTY,
                                "Dalvik").ToString();

                            if (LIB_DALVIK.Equals(value))
                            {
                                return AndroidRuntimeType.Dalvik;
                            }
                            else if (LIB_ART.Equals(value))
                            {
                                return AndroidRuntimeType.ART;
                            }
                            else if (LIB_ART_D.Equals(value))
                            {
                                return AndroidRuntimeType.ART_Debug;
                            }

                            Log.Warn("RuntimeType", string.Format("Unknown runtime lib '{0}'", value));
                            return AndroidRuntimeType.Unknown;
                        }
                        catch (IllegalAccessException e)
                        {
                            Log.Error("RuntimeType", e.Message);
                        }
                        catch (IllegalArgumentException e)
                        {
                            Log.Error("RuntimeType", e.Message);
                        }
                        catch (InvocationTargetException e)
                        {
                            Log.Error("RuntimeType", e.Message);
                        }

                        
                    }
                    catch (NoSuchMethodException e)
                    {
                        Log.Error("RuntimeType", e.Message);
                    }
                }
                catch (ClassNotFoundException e)
                {
                    Log.Error("RuntimeType", e.Message);
                }

                return AndroidRuntimeType.Unknown;
            }
        }
    }
}