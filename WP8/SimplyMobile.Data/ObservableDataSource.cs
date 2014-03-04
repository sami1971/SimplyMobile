using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Reset)
            {
                return;
            }

            foreach (var observer in this.observers.OfType<FrameworkElement>())
            {
                observer.Dispatcher.BeginInvoke(() => observer.DataContext = this.Data);
            }

            foreach (var longList in this.observers.OfType<LongListSelector>())
            {
                longList.Dispatcher.BeginInvoke(() => longList.ItemsSource = this.Data);
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
            foreach (var element in notifyCollectionChangedEventArgs.NewItems.OfType<FrameworkElement>())
            {
                element.Dispatcher.BeginInvoke(()=> element.DataContext = this.Data);
            }

            foreach (var longList in this.observers.OfType<LongListSelector>())
            {
                longList.Dispatcher.BeginInvoke(() => longList.ItemsSource = this.Data);
            }
        }

    }
}
