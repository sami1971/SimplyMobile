using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimplyMobile.Data
{
    public interface IObservableDataSource<T>
    {
        /// <summary>
        /// Occurs when item is selected.
        /// </summary>
        event EventHandler<EventArgs<T>> OnSelected;

        /// <summary>
        /// The requested event occurs when an observer requests an item.
        /// </summary>
        event EventHandler<EventArgs<int>> OnRequested;

        ObservableCollection<T> Data { get; set; }

        Predicate<T> Filter { set; }

        void Bind(object observer);

        bool Unbind(object observer);

        void Add(T item);

        bool Replace(T original, T replacement);

        void Remove(T item);

        

        //void ClearFilter();
    }
}
