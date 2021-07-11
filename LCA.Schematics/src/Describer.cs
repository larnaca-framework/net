using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace LCA.Schematics
{
    /// <summary>
    /// Class responsible 
    /// </summary>
    public class Describer
    {
        public static Describer Default { get; } = new Describer();
        public Describer() : this(new Model(), new HashSet<Assembly>() { typeof(object).Assembly })
        { }
        public Describer(Model model, HashSet<Assembly> frameworkAssemblies)
        {
            _model = model ?? throw new ArgumentNullException(nameof(Model));
            _frameworkAssemblies = frameworkAssemblies ?? throw new ArgumentNullException(nameof(frameworkAssemblies));
        }
        private Model _model;
        private HashSet<Assembly> _frameworkAssemblies;
        public TypeRef GetRef<T>(T obj)
        {
            if (obj is Type type)
            {
                return GetRef(type);
            }
            return GetRef(typeof(T));
        }
        public TypeRef GetRef<T>() => GetRef(typeof(T));
        public TypeRef GetRef(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_model.TypeRefs.TryGetValue(type, out var existing))
            {
                return existing;
            }

            if (type.IsArray)
            {
                return GetRef(type.GetElementType()!)
                    .PrependArrayDimension(type.GetArrayRank());
            }
            var builder = TypeRefBuilder.Start()
                .WithKind(GetTypeRefKind(type))
                .WithName(type.Name.Split('`').First())
                .WithIsFramework(_frameworkAssemblies.Contains(type.Assembly));
            if (builder.Kind != ETypeRefKind.GenericParameter)
            {
                builder.WithNamespace(type.Namespace);
                if (type.IsNested)
                {
                    builder.WithNestedIn(GetRef(type.DeclaringType!));
                }
            }

            if (type.IsGenericType)
            {
                builder.WithGenericArguments(type.GetGenericArguments().Select(t => GetRef(t)).ToArray());
            }
            var theReturn = builder.Build(); ;
            _model.AddTypeRef(type, theReturn);
            return theReturn;
        }
        public bool TryOutlineType(Type type, out TypeRef typeRef, [NotNullWhen(true)] TypeOutline? outline)
        {
            typeRef = GetRef(type);
            if (typeRef.IsFramework || (int)typeRef.Kind > 2) // class, interface or enum
            {
                return false;
            }
            throw new NotImplementedException();
        }
        public static ETypeRefKind GetTypeRefKind(Type type)
        {
            if (type.IsGenericParameter)
            {
                return ETypeRefKind.GenericParameter;
            }
            return (ETypeRefKind)GetTypeKind(type);
        }
        public static ETypeKind GetTypeKind(Type type) =>
        type switch
        {
            { IsEnum: true } => ETypeKind.Enum,
            { IsInterface: true } => ETypeKind.Interface,
            { IsClass: true } => ETypeKind.Class,
            { IsPrimitive: true } => ETypeKind.Primitive,
            { IsValueType: true } => ETypeKind.Struct,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unkown type: {type}")
        };
    }
}