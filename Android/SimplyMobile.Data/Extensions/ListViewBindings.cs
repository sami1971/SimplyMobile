using System;
using System.ComponentModel;
using Android.Widget;

namespace SimplyMobile.Data
{
    public static class ListViewBindings
    {
        public static void Bind<T>(this ListView listView, ObservableDataSource<T> source)
        {
            source.Bind(listView);
        }

        public static void Unbind<T>(this ListView listView, ObservableDataSource<T> source)
        {
            source.Unbind (listView);
        }

        public static void Bind<T>(this ListView listView, int id, INotifyPropertyChanged model, string propertyName = null)
        {
            //source.Bind(listView);
        }

        public static void Unbind<T>(this ListView listView, int id, INotifyPropertyChanged model, string propertyName = null)
        {
            //source.Unbind (listView);
        }
    }
}

