using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Utilities.Extensions
{
    // https://code.google.com/p/morelinq/source/browse/MoreLinq/DistinctBy.cs
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key_selector)
        {
            return source.DistinctBy(key_selector, null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key_selector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (key_selector == null) throw new ArgumentNullException("key_selector");
            return DistinctByImpl(source, key_selector, comparer);
        }

        private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> key_selector, IEqualityComparer<TKey> comparer)
        {
            var known_keys = new HashSet<TKey>(comparer);
            return source.Where(element => known_keys.Add(key_selector(element)));
        }


        public static void Apply<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
