using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LCA.Schematics
{
    /// <summary>
    /// A collection of inter-referenced or just related schematics
    /// </summary>
    public class Model
    {
        private Dictionary<Type, TypeRef> _typeRefs;
        private Dictionary<TypeRef, TypeOutline> _outlines;
        public Model() : this(new Dictionary<Type, TypeRef>(), new Dictionary<TypeRef, TypeOutline>())
        { }
        public Model(Dictionary<Type, TypeRef> typeRefs, Dictionary<TypeRef, TypeOutline> outlines)
        {
            _typeRefs = typeRefs ?? throw new ArgumentNullException(nameof(typeRefs));
            _outlines = outlines ?? throw new ArgumentNullException(nameof(outlines));
        }
        public IReadOnlyDictionary<Type, TypeRef> TypeRefs { get => _typeRefs; }
        public IReadOnlyDictionary<TypeRef, TypeOutline> Outlines { get => _outlines; }
        public bool AddTypeRef(Type type, TypeRef typeRef) =>
            _typeRefs.TryAdd(type ?? throw new ArgumentNullException(nameof(type)), typeRef ?? throw new ArgumentNullException(nameof(typeRef)));
        public bool AddOutline(TypeOutline outline) =>
            _outlines.TryAdd(outline.Ref, outline ?? throw new ArgumentNullException(nameof(outline)));
    }
}
