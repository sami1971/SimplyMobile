using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    /// <summary>
    /// Portable interface for device screen information
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Gets the screen height in pixels
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the screen width in pixels
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the screens X pixel density per inch
        /// </summary>
        double Xdpi { get; }

        /// <summary>
        /// Gets the screens Y pixel density per inch
        /// </summary>
        double Ydpi { get; }
    }

    public static class ScreenExtensions
    {
        public static double ScreenSizeInches(this IScreen screen)
        {
            return Math.Sqrt(Math.Pow(screen.ScreenWidthInches(), 2) + Math.Pow(screen.ScreenHeightInches(), 2));
        }

        public static double ScreenWidthInches(this IScreen screen)
        {
            return screen.Width / screen.Xdpi;
        }

        public static double ScreenHeightInches(this IScreen screen)
        {
            return screen.Height / screen.Ydpi;
        }
    }
}
