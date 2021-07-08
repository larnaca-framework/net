using System;

namespace LCA.Schematics
{
    public class TypeMember : ICloneable
    {
        public TypeMember(
            string name,
            TypeRef? type = default,
            decimal? enumValue = default,
            TypeRef? typeParameter = default
        )
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            EnumValue = enumValue;
            TypeParameter = typeParameter;
        }
        public string Name { get; private set; }
        /// <summary>
        /// null for enums
        /// </summary>
        public TypeRef? Type { get; private set; }
        /// <summary>
        /// Only relvant for enums
        /// </summary>
        public decimal? EnumValue { get; private set; }
        /// <summary>
        /// If this member is a generic parameter in a constructed generic type, then this will have the reference to that generic type parameter
        /// </summary>
        /// <value></value>
        public TypeRef? TypeParameter { get; private set; }
        public TypeMember Clone() => new TypeMember(
            Name,
            Type,
            EnumValue,
            TypeParameter
        );
        object ICloneable.Clone() => Clone();
    }
}
