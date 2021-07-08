using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LCA.Blueprints
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Checks that the collection is null or does not contains at least one element.
        /// Unlike .Any() will NOT throw null exception
        /// </summary>
        public static bool IsNullOrEmpty(this IEnumerable? target)
        {
            if (target == null)
            {
                return true;
            }

            // Since we don't know the implementation details of the collection we have to go with the safest and fastest check
            // without relying on .Count()
            return !target.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Checks that the collection is not null and contains at least one element.
        /// Unlike .Any() will NOT throw null exception
        /// A wrapper for !.IsNullOrEmpty() extension
        /// </summary>
        public static bool IsAny<T>(this IEnumerable<T>? target)
            => !target.IsNullOrEmpty();

        // Enumerable.Empty contains a static, empty, enumeration so  it's not instantiated per call
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? target)
            => target ?? Enumerable.Empty<T>();

        public static T[] Concat<T>(this T[] array1, T[] array2)
        {
            T[] theReturn = new T[array1.Length + array2.Length];
            Array.Copy(array1, theReturn, array1.Length);
            Array.Copy(array2, 0, theReturn, array1.Length, array2.Length);
            return theReturn;
        }
        public static int GetArrayHashCode<T>(this T[]? array, IEqualityComparer<T>? comparer = default)
            => ((IStructuralEquatable)array!).GetHashCode((IEqualityComparer)(comparer ?? EqualityComparer<T>.Default));

        public static bool SequenceEqualOrBothNull<T>(this IEnumerable<T>? a, IEnumerable<T>? b, IEqualityComparer<T>? comparer = default)
        {
            if (a is null || b is null)
            {
                return (a is null && b is null);
            }
            return a.SequenceEqual(b, comparer);
        }
    }
}
