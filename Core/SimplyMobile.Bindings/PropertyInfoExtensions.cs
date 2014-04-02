using System;
using System.Reflection;
using System.Globalization;

namespace SimplyMobile.Bindings
{
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets formatted text of the property.
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="property">Property.</param>
        /// <param name="source">Source.</param>
        /// <param name="formatProvider">Format provider, if null then CultureInfo.CurrentCulture will be used.</param>
        public static string GetText(this PropertyInfo property, object source, IFormatProvider formatProvider = null)
        {
            return string.Format(formatProvider ?? CultureInfo.CurrentCulture, "{0}", property.GetValue(source));
        }
    }
}

