using System;
using Android.Views;

namespace SimplyMobile.Data
{
    public class DropDownCellDelegate<T> : IDropDownCellProvider<T>
    {
        private readonly Func<T, View, View> dropDownDelegate;

        public DropDownCellDelegate (Func<T, View, View> dropDownDelegate)
        {
            this.dropDownDelegate = dropDownDelegate;
        }

        #region IDropDownCellProvider implementation
        public View GetDropDownView(T item, View convertView)
        {
            return this.dropDownDelegate (item, convertView);
        }

        #endregion
    }
}

