using System;
using System.Collections.Generic;
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
        private Dictionary<Type, TypeRef> _refs = new Dictionary<Type, TypeRef>();
        private Model _model = new Model();
        private HashSet<Assembly> _frameworkAssemblies = new HashSet<Assembly>()
        {
            typeof(object).Assembly,
            typeof(List<>).Assembly
        };
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

            if (_refs.TryGetValue(type, out var existing))
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

            return _refs[type] = builder.Build();
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