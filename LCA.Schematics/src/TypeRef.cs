using System;
using System.Linq;

namespace LCA.Schematics
{
    public class TypeRef : ICloneable
    {
        public ETypeRefKind Kind { get; set; }
        public string Name { get; set; }
        public string? Namespace { get; set; }
        public TypeRef? NestedIn { get; set; }
        /// <summary>
        /// a non-array is denoted as null/empty
        /// a one-dimensional array (e.g. foo[]) is denoted as {1}
        /// a multi-dimensional jagged array (e.g. foo[,][][,,]) is denoted as {2,1,3}
        /// </summary>
        public int[]? ArrayDimensions { get; set; }
        /// <summary>
        /// - If the current type is a closed constructed type (that is, the ContainsGenericParameters property returns false), 
        /// the array contains the types that have been assigned to the generic type parameters of the generic type definition.
        /// - If the current type is a generic type definition, the array contains the type parameters.
        /// - If the current type is an open constructed type (that is, the ContainsGenericParameters property returns true) 
        /// in which specific types have not been assigned to all of the type parameters and type parameters of enclosing generic types, 
        /// the array contains both types and type parameters
        /// </summary>
        public TypeRef[]? GenericArguments { get; set; }

        /// <summary>
        /// type is included in the net 5.0 framework, e.g. List<> or a primitive such as bool
        /// </summary>
        public bool IsFramework { get; set; }
        object ICloneable.Clone() => Clone();
        public TypeRef Clone() => new TypeRef()
        {
            Name = Name,
            Namespace = Namespace,
            NestedIn = NestedIn?.Clone(),
            ArrayDimensions = ArrayDimensions,
            GenericArguments = GenericArguments?.Select(a => a.Clone()).ToArray(),
            IsFramework = IsFramework
        };
    }
}