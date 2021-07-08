using System;
using System.Linq;

namespace LCA.Schematics
{
    /// <summary>
    /// The outline/schema of a type, the actual definition of the type with all it's members
    /// </summary>
    public class TypeOutline : ICloneable
    {
        public TypeOutline(
            TypeRef @ref,
            ETypeKind kind,
            TypeMember[] members,
            TypeRef? @base = null,
            TypeRef[]? implementedInterfaces = null)
        {
            Ref = @ref ?? throw new ArgumentNullException(nameof(@ref));
            Kind = kind;
            Members = members ?? throw new ArgumentNullException(nameof(members));
            Base = @base;
            ImplementedInterfaces = implementedInterfaces;
        }
        public TypeRef Ref { get; private set; }
        public ETypeKind Kind { get; private set; }
        public TypeMember[] Members { get; private set; }
        public TypeRef? Base { get; private set; }
        public TypeRef[]? ImplementedInterfaces { get; private set; }
        public TypeOutline Clone() => new TypeOutline(
            Ref.Clone(),
            Kind,
            Members.Select(m => m.Clone()).ToArray(),
            Base?.Clone(),
            ImplementedInterfaces?.Select(r => r.Clone()).ToArray()
            );
        object ICloneable.Clone() => Clone();
    }
}