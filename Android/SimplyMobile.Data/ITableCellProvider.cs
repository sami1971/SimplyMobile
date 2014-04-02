using System;
using Android.Views;

namespace SimplyMobile.Data
{
    /// <summary>
    /// Table cell provider interface for Android View's
    /// </summary>
    /// <description>
    /// Implement this interface in your ListView or Spinner class to override
    /// the default table cell view in your application.
    /// 
    /// NOTE: Implementing this in other than the table view bind to the
    /// data source will not have any effect.
    /// </description>
    public interface ITableCellProvider<T>
    {
        /// <summary>
        /// Gets the Android View cell to display in your list/spinner view.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="item">Item to bind with.</param>
        /// <param name="convertView">Convert view.</param>
        View GetView(T item, View convertView);
    }
}

