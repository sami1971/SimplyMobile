using System;
using Android.Views;

namespace SimplyMobile.Data
{
    public class TableCellDelegate<T> : ITableCellProvider<T>
    {
        public delegate View GetViewDelegate(T item, View convertView);

        private readonly GetViewDelegate getView;

        public TableCellDelegate (GetViewDelegate getView)
        {
            this.getView = getView;
        }

        #region ITableCellProvider implementation

        public View GetView(T item, View convertView)
        {
            return this.getView (item, convertView);
        }

        #endregion
    }
}

