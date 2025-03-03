using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeKtvi.Toolkit.Tests
{
    public class ComparableDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        public ComparableDictionary()
        {
        }

        public ComparableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public ComparableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
        {
        }

        public ComparableDictionary(IEqualityComparer<TKey>? comparer) : base(comparer)
        {
        }

        public ComparableDictionary(int capacity) : base(capacity)
        {
        }

        public ComparableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) : base(dictionary, comparer)
        {
        }

        public ComparableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer)
        {
        }

        public ComparableDictionary(int capacity, IEqualityComparer<TKey>? comparer) : base(capacity, comparer)
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            if (obj is IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
                return Enumerable.SequenceEqual(this, enumerable);

            return false;
        }

        public override int GetHashCode() => 0;
    }
}
