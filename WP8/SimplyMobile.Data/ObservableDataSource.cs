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

            var refs = this.observers.Where(a => a.IsAlive).Select(a => a.Target).ToList();

            foreach (var observer in refs.OfType<FrameworkElement>())
            {
                observer.Dispatcher.BeginInvoke(() => observer.DataContext = this.Data);
            }

            foreach (var longList in refs.OfType<LongListSelector>())
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
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                var refs = notifyCollectionChangedEventArgs.NewItems.OfType<WeakReference>().Where(a => a.IsAlive).Select(a => a.Target).ToList();

                foreach (var element in refs.OfType<FrameworkElement>())
                {
                    element.Dispatcher.BeginInvoke(() => element.DataContext = this.Data);
                }

                foreach (var longList in refs.OfType<LongListSelector>())
                {
                    longList.Dispatcher.BeginInvoke(() => longList.ItemsSource = this.Data);
                    longList.SelectionChanged += longList_SelectionChanged;
                }
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                var refs = notifyCollectionChangedEventArgs.OldItems.OfType<WeakReference>().Where(a => a.IsAlive).Select(a => a.Target).ToList();
                foreach (var element in refs.OfType<FrameworkElement>())
                {
                    element.Dispatcher.BeginInvoke(() => element.DataContext = null);
                }

                foreach (var longList in refs.OfType<LongListSelector>())
                {
                    longList.Dispatcher.BeginInvoke(() => longList.ItemsSource = null);
                    longList.SelectionChanged -= this.longList_SelectionChanged;
                }
            }
        }

        void longList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach(var item in e.AddedItems.OfType<T>())
            {
                this.InvokeItemSelectedEvent(sender, item);
            }
        }

    }
}
