using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LCA.Blueprints;

namespace LCA.Schematics
{
    /// <summary>
    /// A reference to a type, does not actually contain the type definition, but the definition can be found using it
    /// </summary>
    public class TypeRef : ICloneable, IEquatable<TypeRef>
    {
        public TypeRef(
            ETypeRefKind kind,
            string name,
            bool isFramework = false,
            string? @namespace = default,
            TypeRef? nestedIn = default,
            int[]? arrayDimensions = default,
            TypeRef[]? genericArguments = default)
        {
            Kind = kind;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsFramework = isFramework;
            Namespace = @namespace;
            NestedIn = nestedIn;
            ArrayDimensions = arrayDimensions;
            GenericArguments = genericArguments;

            if (arrayDimensions.IsAny())
            {
                SimpleRef = Clone();
                SimpleRef.ArrayDimensions = null;
            }
            else
            {
                SimpleRef = this;
            }
        }
        public ETypeRefKind Kind { get; private set; }
        /// <summary>
        /// can be an empty string for a 
        /// </summary>
        /// <value></value>
        public string Name { get; private set; }
        /// <summary>
        /// type is included in the net 5.0 framework, e.g. <![CDATA[ List<> ]]> or a primitive such as bool
        /// </summary>
        public bool IsFramework { get; private set; }
        public string? Namespace { get; private set; }
        public TypeRef? NestedIn { get; private set; }
        /// <summary>
        /// a non-array is denoted as null/empty
        /// a one-dimensional array (e.g. foo[]) is denoted as {1}
        /// a multi-dimensional jagged array (e.g. foo[,][][,,]) is denoted as {2,1,3}
        /// </summary>
        public int[]? ArrayDimensions { get; private set; }
        /// <summary>
        /// - If the current type is a closed constructed type (that is, the ContainsGenericParameters property returns false), 
        /// the array contains the types that have been assigned to the generic type parameters of the generic type definition.
        /// - If the current type is a generic type definition, the array contains the type parameters.
        /// - If the current type is an open constructed type (that is, the ContainsGenericParameters property returns true) 
        /// in which specific types have not been assigned to all of the type parameters and type parameters of enclosing generic types, 
        /// the array contains both types and type parameters
        /// </summary>
        public TypeRef[]? GenericArguments { get; private set; }

        /// <summary>
        /// A reference to the type in it's most simple form, e.g. without array dimensions 
        /// </summary>
        public TypeRef SimpleRef { get; }
        object ICloneable.Clone() => Clone();
        public TypeRef Clone() => new TypeRef(
            Kind,
            Name,
            IsFramework,
            Namespace,
            NestedIn?.Clone(),
            ArrayDimensions,
            GenericArguments?.Select(a => a.Clone()).ToArray()
        );

        public bool Equals(TypeRef? other)
        {
            if (other is null)
            {
                return false;
            }
            return ReferenceEquals(this, other) || (
                Kind == other.Kind &&
                Name == other.Name &&
                IsFramework == other.IsFramework &&
                Namespace == other.Namespace &&
                NestedIn! == other.NestedIn! &&
                ArrayDimensions.SequenceEqualOrBothNull(other.ArrayDimensions) &&
                GenericArguments.SequenceEqualOrBothNull(other.GenericArguments)
            );
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as TypeRef);
        }

        public override int GetHashCode()
        {
            var theReturn = new HashCode();
            theReturn.Add(Kind);
            theReturn.Add(Name);
            theReturn.Add(IsFramework);
            theReturn.Add(Namespace);
            theReturn.Add(NestedIn);
            theReturn.Add(ArrayDimensions.GetArrayHashCode());
            theReturn.Add(GenericArguments.GetArrayHashCode());
            return theReturn.ToHashCode();
        }

        public static bool operator ==(TypeRef? a, TypeRef? b)
            => a?.Equals(b) ?? b is null;
        public static bool operator !=(TypeRef? a, TypeRef? b)
            => !(a == b);
    }
}
