using System;
using Android.App;
using Android.Util;

namespace SimplyMobile.Device
{
    using Core;

    /// <summary>
    /// Display class.
    /// </summary>
    public static class Display
    {
        /// <summary>
        /// Gets the screen size in inches.
        /// </summary>
        /// <value>The screen size in inches.</value>
        public static double ScreenSizeInches
        {
            get
            {
                return Application.Context.Resources.DisplayMetrics.ScreenSizeInches ();
            }
        }
    }
     
    /// <summary>
    /// Display metrics extensions.
    /// </summary>
    static class DisplayMetricsExtensions
    {
        /// <summary>
        /// Calculates screen size in inches.
        /// </summary>
        /// <returns>The screen size in inches.</returns>
        /// <param name="dm">Display Metrics.</param>
        internal static double ScreenSizeInches(this DisplayMetrics dm)
        {
            return Math.Sqrt (Math.Pow (dm.WidthPixels / dm.Xdpi, 2) + Math.Pow (dm.HeightPixels / dm.Ydpi, 2));
        }
    }
}

