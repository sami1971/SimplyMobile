using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSource<T>
    {
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
            //if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            //{
            //    foreach (var itemsControl in notifyCollectionChangedEventArgs.NewItems.OfType<ItemsControl>())
            //    {
            //        itemsControl.ItemsSource = this.Data;
            //    }
            //}
        }

    }
}
