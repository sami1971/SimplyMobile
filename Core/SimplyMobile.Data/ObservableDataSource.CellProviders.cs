using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSource<T>
    {
        private Dictionary<WeakReference<object>, ITableCellProvider<T>> tableCellProviders;

        private Dictionary<WeakReference<object>, ITableCellProvider<T>> TableCellProviders
        {
            get { return tableCellProviders ?? (tableCellProviders = new Dictionary<WeakReference<object>, ITableCellProvider<T>>()); }
        }

        public void SetCellProvider(object table, ITableCellProvider<T> provider)
        {
            var type = new WeakReference<object>(table);
            this.TableCellProviders.Remove (type);
            this.TableCellProviders.Add (type, provider);
        }

        private ITableCellProvider<T> FindProvider(object table)
        {
            return this.TableCellProviders.Where(a => 
                {
                    object o;
                    return a.Key.TryGetTarget(out o) && ReferenceEquals(o, table);
                }).Select(b=>b.Value).FirstOrDefault();
        }
    }
}
