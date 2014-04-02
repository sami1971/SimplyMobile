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

namespace SimplyMobile.Runtime
{
    public enum AndroidRuntimeType
    {
        Unknown,
        Dalvik,
        ART,
        ART_Debug
    }
}