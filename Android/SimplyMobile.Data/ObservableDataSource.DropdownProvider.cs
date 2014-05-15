using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSource<T>
    {
        private readonly Lazy<Dictionary<WeakReference<object>, IDropDownCellProvider<T>>> dropdownCellProviders = new Lazy<Dictionary<WeakReference<object>, IDropDownCellProvider<T>>>();

        public void SetProvider(object table, IDropDownCellProvider<T> provider)
        {
            var type = new WeakReference<object>(table);
            this.dropdownCellProviders.Value.Remove (type);
            this.dropdownCellProviders.Value.Add (type, provider);
        }

        private IDropDownCellProvider<T> FindDropDownProvider(object table)
        {
            return this.dropdownCellProviders.Value.Where(a => 
            {
                object o;
                return a.Key.TryGetTarget(out o) && ReferenceEquals(o, table);
            }).Select(b=>b.Value).FirstOrDefault();
        }
    }
}

