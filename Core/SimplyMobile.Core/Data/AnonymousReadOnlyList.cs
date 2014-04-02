using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile
{
    public sealed class AnonymousReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly Func<int> _count;
        private readonly Func<int, T> _item;
        private readonly IEnumerable<T> _iterator;

        public AnonymousReadOnlyList(Func<int> count, Func<int, T> item, IEnumerable<T> iterator = null)
        {
            if (count == null) throw new ArgumentNullException("count");
            if (item == null) throw new ArgumentNullException("item");
            this._count = count;
            this._item = item;
            this._iterator = iterator ?? DefaultIterator(count, item);
        }
        private static IEnumerable<T> DefaultIterator(Func<int> count, Func<int, T> item)
        {
            var n = count();
            for (var i = 0; i < n; i++)
                yield return item(i);
        }

        public int Count
        {
            get
            {
                return _count();
            }
        }
        public T this[int index]
        {
            get
            {
                return _item(index);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _iterator.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
