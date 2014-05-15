using System;
using System.Collections.Specialized;
using System.Linq;

using Android.Views;
using Android.Widget;
using Android.App;

namespace SimplyMobile.Data
{
    /// <summary>
    /// Observable data source Android portion.
    /// </summary>
    public partial class ObservableDataSource<T> : Java.Lang.Object, IListAdapter, ISpinnerAdapter
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
            this.InvokeItemRequestedEvent (parent, position);

            var item = this.Data [position];
            var cellProvider = parent as ITableCellProvider<T> ?? this.FindProvider(parent);

            if (cellProvider != null)
            {
                return cellProvider.GetView (item, convertView);
            }

            var v = convertView as TextView ?? new TextView(parent.Context);
            v.Text = item.ToString();
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
            var item = this.Data [position];

            var cellProvider = parent as IDropDownCellProvider<T> ?? this.FindDropDownProvider (parent);
            return cellProvider != null ? cellProvider.GetDropDownView (item, convertView) : this.GetView (position, convertView, parent);
        }

        /// <summary>
        /// Handles the item selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandleItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            this.InvokeItemSelectedEvent(sender, this.Data[e.Position]);
        }

        private void HandleItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.InvokeItemSelectedEvent(sender, this.Data[e.Position]);
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
            foreach (var listView in this.observers.Where(a=>a.IsAlive).Select(a=>a.Target).OfType<ListView>())
            {
                ((Activity)listView.Context).RunOnUiThread (listView.InvalidateViews);
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
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                var refs = notifyCollectionChangedEventArgs.NewItems.OfType<WeakReference>().Where(a=>a.IsAlive).Select(a=>a.Target).ToList();

                foreach (var newListView in refs.OfType<ListView>())
                {
                    newListView.Adapter = this;
                    newListView.ItemSelected -= HandleItemSelected;
                    newListView.ItemSelected += HandleItemSelected;
                    newListView.ItemClick -= HandleItemClicked;
                    newListView.ItemClick += HandleItemClicked;
                }

                foreach (var newSpinner in refs.OfType<Spinner>())
                {
                    newSpinner.Adapter = this;
                    newSpinner.ItemSelected -= HandleItemSelected;
                    newSpinner.ItemSelected += HandleItemSelected;
                    newSpinner.NothingSelected += HandleNothingSelected;
                }
//              foreach (var listView in this.observers.OfType<ListView>())
//              {
//                  listView.Adapter = this;
//              }
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                var refs = notifyCollectionChangedEventArgs.OldItems.OfType<WeakReference>().Where(a=>a.IsAlive).Select(a=>a.Target).ToList();

                foreach (var removedListView in refs.OfType<ListView>())
                {
                    removedListView.Adapter = null;
                    removedListView.ItemSelected -= HandleItemSelected;
                    removedListView.ItemClick -= HandleItemClicked;
                }

                foreach (var removedSpinner in refs.OfType<Spinner>())
                {
                    removedSpinner.Adapter = null;
                    removedSpinner.ItemSelected -= HandleItemSelected;
                }
            }
        }

        void HandleNothingSelected (object sender, AdapterView.NothingSelectedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine ("HandleNothingSelected");
        }

        public void OnItemSelected(AdapterView parent, View view, int position, long id)
        {
            this.HandleItemSelected (parent, new AdapterView.ItemSelectedEventArgs (parent, view, position, id));
        }

        public void OnNothingSelected(Android.Widget.AdapterView parent)
        {
//          throw new NotImplementedException ();
        }

        void HandleClick (object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine ("HandleClick clicked");
        }

        void HandleItemClick (object sender, AdapterView.ItemClickEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine ("HandleItemClick clicked");
        }
    }
}