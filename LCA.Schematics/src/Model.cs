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
        private Dictionary<TypeRef, TypeOutline> _outlines;
        public Model() : this(new Dictionary<TypeRef, TypeOutline>()) { }
        public Model(Dictionary<TypeRef, TypeOutline> outlines)
        {
            _outlines = outlines ?? throw new System.ArgumentNullException(nameof(outlines));
        }
        public IReadOnlyDictionary<TypeRef, TypeOutline> Outlines { get => _outlines; }
        public bool AddOutline(TypeOutline outline)
        {
            if (outline is null)
            {
                throw new ArgumentNullException(nameof(outline));
            }
            return _outlines.TryAdd(outline.Ref, outline);
        }
        public bool TryGetOutline(TypeRef typeRef, [MaybeNullWhen(false)] out TypeOutline typeOutline)
            => _outlines.TryGetValue(typeRef.SimpleRef ?? throw new ArgumentNullException(nameof(typeRef)), out typeOutline);
    }
}
