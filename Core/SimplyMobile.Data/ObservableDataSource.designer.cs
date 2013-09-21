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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SimplyMobile.Core;

namespace SimplyMobile.Data
{
    /// <summary>
    /// The observable data source.
    /// </summary>
    public partial class ObservableDataSource
    {
        /// <summary>
        /// The data.
        /// </summary>
        private ObservableCollection<object> data;

        /// <summary>
        /// The observers.
        /// </summary>
        private ObservableCollection<object> observers;

        public ObservableDataSource()
        {
            this.data = new ObservableCollection<object>();
            this.observers = new ObservableCollection<object>();
            this.observers.CollectionChanged += ObserversChanged;
        }

        /// <summary>
        /// Occurs when item is selected.
        /// </summary>
        public EventHandler<EventArgs<object>> OnSelected;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public ObservableCollection<object> Data
        {
            get
            {
                if (this.data == null)
                {
                    this.Data = new ObservableCollection<object>();
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
            }
        }

        /// <summary>
        /// Binds datasource to an object
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        public void Bind(object observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
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
