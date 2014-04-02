using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSections
    {
        /// <summary>
        /// The observers.
        /// </summary>
        private ObservableCollection<object> observers;

        public ObservableCollection<ObservableSection> Sections
        {
            get;
            private set;
        }

        public ObservableDataSections()
        {
            this.Sections = new ObservableCollection<ObservableSection> ();
            this.Initialize ();
        }

        public ObservableDataSections(IEnumerable<ObservableSection> sections)
        {
            this.Sections = new ObservableCollection<ObservableSection>(sections);
            this.Initialize ();
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
        /// Unbind the specified observer.
        /// </summary>
        /// <param name="observer">Observer.</param>
        public void Unbind(object observer)
        {
            this.observers.Remove (observer);
        }

        private void Initialize()
        {
            this.Sections.CollectionChanged += HandleCollectionChanged;
            this.observers = new ObservableCollection<object> ();
            this.observers.CollectionChanged += this.ObserversChanged;
        }

        void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems.Cast<ObservableSection>())
                {
                    newItem.Values.CollectionChanged += HandleCollectionChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems.Cast<ObservableSection>())
                {
                    oldItem.Values.CollectionChanged -= HandleCollectionChanged;
                }
            }
        }

        void HandleSectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            this.CollectionChanged (sender, e);
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

