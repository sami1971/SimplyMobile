using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSource<T>
    {
        private Dictionary<Type, ITableCellProvider<T>> tableCellProviders;

        private Dictionary<Type, ITableCellProvider<T>> TableCellProviders
        {
            get { return tableCellProviders ?? (tableCellProviders = new Dictionary<Type, ITableCellProvider<T>> ()); }
        }

        public void SetCellProvider(object table, ITableCellProvider<T> provider)
        {
            var type = table.GetType ();
            this.TableCellProviders.Remove (type);
            this.TableCellProviders.Add (type, provider);
        }
    }
}
