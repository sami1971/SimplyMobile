//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SimplyMobile.Core;
using System.Linq;

namespace SimplyMobile.Data
{
    /// <summary>
    /// The observable data source.
    /// </summary>
    public partial class ObservableDataSource<T> : IObservableDataSource<T>
    {
        private Predicate<T> filter;

        /// <summary>
        /// The data.
        /// </summary>
        private ObservableCollection<T> data;

        /// <summary>
        /// The observers.
        /// </summary>
        private readonly ObservableCollection<WeakReference> observers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDataSource"/> class.
        /// </summary>
        public ObservableDataSource()
        {
            this.observers = new ObservableCollection<WeakReference>();
            this.observers.CollectionChanged += this.ObserversChanged;
            this.Data = new ObservableCollection<T>();
        }

        public ObservableDataSource(IEnumerable<T> data)
        {
            this.observers = new ObservableCollection<WeakReference>();
            this.observers.CollectionChanged += this.ObserversChanged;
            this.Data = new ObservableCollection<T>(data);
        }

        public Predicate<T> Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Occurs when item is selected.
        /// </summary>
        public event EventHandler<EventArgs<T>> OnSelected;

        /// <summary>
        /// The requested event occurs when an observer requests an item.
        /// </summary>
        /// <remarks>The sender will be the requesting observer, f.e. a ListView in Android
        /// or UITableView in iOS.</remarks>
        public event EventHandler<EventArgs<int>> OnRequested;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public ObservableCollection<T> Data
        {
            get
            {
                if (this.data == null)
                {
                    this.Data = new ObservableCollection<T>();
                }

                return this.data;
            }

            set
            {
                if (this.data != null)
                {
                    this.data.CollectionChanged -= this.CollectionChanged;
                }

                this.data = value;
                this.data.CollectionChanged += this.CollectionChanged;

                this.CollectionChanged (this, new NotifyCollectionChangedEventArgs (NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Binds data source to an object
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        /// <remarks>
        /// The observer should implement <see cref="ITableCellProvider"/> interface.
        /// </remarks>
        public void Bind(object observer)
        {
            if (!this.observers.Any(a=> a.IsAlive && a.Target.Equals(observer)))
            {
                this.observers.Add(new WeakReference(observer));
            }
        }

        /// <summary>
        /// Unbind the specified observer.
        /// </summary>
        /// <param name="observer">Observer to unbind.</param>
        /// <returns>true when unbind is successful, otherwise false</returns>
        public bool Unbind(object observer)
        {
            var l = this.observers.Where (a => a.Target.Equals (observer)).ToList ();
            foreach (var item in l)
            {
                this.observers.Remove (item);
            }

            return true;
        }

        /// <summary>
        /// Add items to data collection.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(T item)
        {
            this.Data.Add(item);
        }

        /// <summary>
        /// Replaces an object in collection.
        /// </summary>
        /// <param name="original">
        /// The original object.
        /// </param>
        /// <param name="replacement">
        /// The replacement object.
        /// </param>
        /// <returns>
        /// <see cref="bool"/>, true if replacement was successful, false if original object was not found.
        /// </returns>
        public bool Replace(T original, T replacement)
        {
            var index = this.Data.IndexOf(original);

            if (index < 0)
            {
                return false;
            }

            this.Data[index] = replacement;

            return true;
        }

        public void Remove(T item)
        {
            this.Data.Remove(item);
        }

        //public void ClearFilter();

        /// <summary>
        /// Invokes the item selected event.
        /// </summary>
        /// <param name="item">Item.</param>
        private void InvokeItemSelectedEvent(object sender, T item)
        {
            if (this.OnSelected != null)
            {
                this.OnSelected.Invoke(sender, new EventArgs<T>(item));
            }
        }

        /// <summary>
        /// Invokes the item requested event.
        /// </summary>
        /// <param name="index">Index of the requested item.</param>
        private void InvokeItemRequestedEvent(object sender, int index)
        {
            if (this.OnRequested != null)
            {
                this.OnRequested (sender, new EventArgs<int> (index));
            }
        }

        /// <summary>
        /// The collection changed partial method must be implemented in the OS specific code.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs);

        /// <summary>
        /// Partial class to be implemented in the OS specific code.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void ObserversChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs);
    }
}
