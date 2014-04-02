using System;
using System.Collections.Specialized;
using Android.Widget;
using System.Linq;
using Android.Views;
using System.Collections.Generic;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSections : Java.Lang.Object, IListAdapter, ISpinnerAdapter
    {
        private List<object> items = new List<object> ();

        /// <summary>
        /// The collection changed partial method must be implemented in the OS specific code.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            this.items = new List<object>();

            foreach (var section in this.Sections)
            {
                items.Add(section);
                items.Add(section.Values);
            }

            foreach (var listView in this.observers.OfType<ListView>())
            {
            
            }
        }

        /// <summary>
        /// Partial class to be implemented in the OS specific code.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void ObserversChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {

        }

        public bool AreAllItemsEnabled()
        {
            return true;
        }

        public bool IsEnabled(int position)
        {
            return true;
        }

        public int Count
        {
            get { return this.Sections.Select(a=>a.Values.Count).Sum() + this.Sections.Count; }
        }

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

            var item = this.items [position];

            var section = item as ObservableSection;
            if (section != null)
            {
                return GetHeader (section.Section, convertView, parent);
            }

            return GetView (item, convertView, parent);
        }

        protected virtual View GetHeader(object item, View convertView, ViewGroup parent)
        {
            var v = convertView as TextView ?? new TextView(parent.Context);
            v.Text = item.ToString();
            return v;
        }

        protected virtual View GetView(object item, View convertView, ViewGroup parent)
        {
            var v = convertView as TextView ?? new TextView(parent.Context);
            v.SetPadding (10, 0, 0, 0);
            v.Text = item.ToString();
            return v;
        }

        public virtual bool HasStableIds
        {
            get { return true; }
        }

        public virtual bool IsEmpty
        {
            get { return !this.Sections.Any(); }
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
    }
}

