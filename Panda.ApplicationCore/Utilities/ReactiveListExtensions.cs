using System.Collections.Generic;
using ReactiveUI;

namespace Panda.ApplicationCore.Utilities
{
    public static class ReactiveListExtensions
    {
        public static ReactiveList<T> ToReactiveList<T>(this IEnumerable<T> source)
        {
            return new ReactiveList<T>(source);
        }
    }
}
