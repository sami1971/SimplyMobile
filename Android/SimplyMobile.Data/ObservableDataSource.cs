using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimplyMobile.Data
{
    /// <summary>
    /// Observable data source.
    /// </summary>
    public partial class ObservableDataSource : Java.Lang.Object, IListAdapter, ISpinnerAdapter
    {
        /// <summary>
        /// The are all items enabled.
        /// </summary>
        /// <returns>
        /// <see cref="bool"/>.
        /// </returns>
        public bool AreAllItemsEnabled()
        {
            return true;
        }

        /// <summary>
        /// Return whether item is enabled.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEnabled(int position)
        {
            return true;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this.Data.Count; }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="Object"/>.
        /// </returns>
        public Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public long GetItemId(int position)
        {
            return position;
        }

        public int GetItemViewType(int position)
        {
            return position;
        }

        public virtual View GetView(int position, View convertView, ViewGroup parent)
        {
            var v = convertView as TextView ?? new TextView(parent.Context);
            v.Text = this.Data[position].ToString();
            return v;
        }

        public virtual bool HasStableIds
        {
            get { return true; }
        }

        public virtual bool IsEmpty
        {
            get { return !this.Data.Any(); }
        }

        public virtual void RegisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            //throw new NotImplementedException();
        }

        public virtual void UnregisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            //throw new NotImplementedException();
        }

        public virtual int ViewTypeCount
        {
            get { return 1; }
        }

        public virtual View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            return this.GetView(position, convertView, parent);
        }

        /// <summary>
        /// The collection changed partial method Android implementation.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (var listView in this.observers.OfType<ListView>())
            {
				listView.InvalidateViews();
            }
        }

        /// <summary>
        /// Observers changed partial method Android implementation.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// Changed observer information.
        /// </param>
        partial void ObserversChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
			foreach (var listView in this.observers.OfType<ListView>())
			{
				listView.Adapter = this;
			}
        }
    }
}