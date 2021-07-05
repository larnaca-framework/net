using System;
using System.Collections.Generic;

namespace LCA.Blueprints
{
    /// <summary>
    /// A generic object comparerer that would only use object's reference, 
    /// ignoring any <see cref="IEquatable{T}"/> or <see cref="object.Equals(object)"/>  overrides.
    /// </summary>
    public class ObjectReferenceEqualityComparer<T> : EqualityComparer<T>
        where T : class
    {
        private static readonly IEqualityComparer<T> _defaultComparer = new ObjectReferenceEqualityComparer<T>();

        public new static IEqualityComparer<T> Default => _defaultComparer;

        public override bool Equals(T? x, T? y)
        {
            return ReferenceEquals(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }
}
