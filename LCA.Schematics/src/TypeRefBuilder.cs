using System;
using System.Linq;
using LCA.Blueprints;

namespace LCA.Schematics
{
    public class TypeRefBuilder :
        TypeRefBuilder.IStart,
        TypeRefBuilder.IName,
        TypeRefBuilder.IOptionalWithNamespace,
        TypeRefBuilder.IOptional
    {
        public ETypeRefKind Kind { get; private set; }

        // will be initialized by the builder in the WithName(...) method
#nullable disable
        public string Name { get; private set; }
#nullable enable
        public bool IsFramework { get; private set; }
        public string? Namespace { get; private set; }
        public TypeRef? NestedIn { get; private set; }
        public int[]? ArrayDimensions { get; private set; }
        public TypeRef[]? GenericArguments { get; private set; }
        public static IStart Start() => new TypeRefBuilder();
        public IOptional From(TypeRef typeRef)
        {
            Kind = typeRef.Kind;
            Name = typeRef.Name;
            IsFramework = typeRef.IsFramework;
            Namespace = typeRef.Namespace;
            NestedIn = typeRef.NestedIn?.Clone();
            ArrayDimensions = typeRef.ArrayDimensions;
            GenericArguments = typeRef.GenericArguments?.Select(a => a.Clone()).ToArray();
            return this;
        }
        IName IStart.WithKind(ETypeRefKind kind)
        {
            if (!Enum.IsDefined<ETypeRefKind>(kind))
            {
                throw new ArgumentOutOfRangeException(nameof(kind), $"The kind {kind} is not defained in enum {nameof(ETypeRefKind)}");
            }
            Kind = kind;
            return this;
        }
        IOptionalWithNamespace IName.WithName(string name)
        {
            Name = name;
            return this;
        }
        IOptional IName.WithName(string? @namespace, string name)
        {
            Namespace = @namespace;
            Name = name;
            return this;
        }

        IOptional IOptionalWithNamespace.WithNamespace(string? @namespace)
        {
            Namespace = @namespace;
            return this;
        }
        IOptional IOptional.WithArrayDimensions(int[] dimensions) => (IOptional)(this as IOptionalWithNamespace).WithArrayDimensions(dimensions);
        IOptionalWithNamespace IOptionalWithNamespace.WithArrayDimensions(int[] dimensions)
        {
            ArrayDimensions = ArrayDimensions?.Concat(dimensions) ?? dimensions;
            return this;
        }
        IOptional IOptional.WithGenericArguments(params TypeRef[] arguments) => (IOptional)(this as IOptionalWithNamespace).WithGenericArguments(arguments);
        IOptionalWithNamespace IOptionalWithNamespace.WithGenericArguments(params TypeRef[] arguments)
        {
            GenericArguments = GenericArguments?.Concat(arguments) ?? arguments;
            return this;
        }
        IOptional IOptional.WithGenericParameters(params string[] parameters) => (IOptional)(this as IOptionalWithNamespace).WithGenericParameters(parameters);
        IOptionalWithNamespace IOptionalWithNamespace.WithGenericParameters(params string[] parameters)
        {
            var typedParameters = parameters.Select(p => new TypeRef(ETypeRefKind.GenericParameter, p, false)).ToArray();
            GenericArguments = GenericArguments?.Concat(typedParameters) ?? typedParameters;
            return this;
        }
        IOptional IOptional.WithNestedIn(TypeRef nestedId) => (IOptional)(this as IOptionalWithNamespace).WithNestedIn(nestedId);
        IOptionalWithNamespace IOptionalWithNamespace.WithNestedIn(TypeRef nestedId)
        {
            NestedIn = nestedId;
            return this;
        }
        IOptional IOptional.WithIsFramework(bool isFramework) => (IOptional)(this as IOptionalWithNamespace).WithIsFramework(isFramework);
        IOptionalWithNamespace IOptionalWithNamespace.WithIsFramework(bool isFramework)
        {
            IsFramework = isFramework;
            return this;
        }
        TypeRef IOptional.Build() => (this as IOptionalWithNamespace).Build();
        TypeRef IOptionalWithNamespace.Build() => new TypeRef(
                Kind,
                Name,
                IsFramework,
                Namespace,
                NestedIn,
                ArrayDimensions,
                GenericArguments
            );
        public interface IStart
        {
            IOptional From(TypeRef typeRef);
            IName WithKind(ETypeRefKind kind);
        }
        public interface IName : IStart
        {
            public ETypeRefKind Kind { get; }
            IOptionalWithNamespace WithName(string name);
            IOptional WithName(string? @namespace, string name);
        }
        public interface IOptionalWithNamespace : IName
        {
            bool IsFramework { get; }
            TypeRef? NestedIn { get; }
            int[]? ArrayDimensions { get; }
            TypeRef[]? GenericArguments { get; }
            IOptional WithNamespace(string? @namespace);
            IOptionalWithNamespace WithArrayDimensions(int[] dimensions);
            IOptionalWithNamespace WithGenericArguments(params TypeRef[] arguments);
            IOptionalWithNamespace WithGenericParameters(params string[] parameters);
            IOptionalWithNamespace WithNestedIn(TypeRef nestedId);
            IOptionalWithNamespace WithIsFramework(bool isFramework);
            TypeRef Build();
        }
        public interface IOptional : IName
        {
            bool IsFramework { get; }
            string? Namespace { get; }
            TypeRef? NestedIn { get; }
            int[]? ArrayDimensions { get; }
            TypeRef[]? GenericArguments { get; }
            IOptional WithArrayDimensions(int[] dimensions);
            IOptional WithGenericArguments(params TypeRef[] arguments);
            IOptional WithGenericParameters(params string[] parameters);
            IOptional WithNestedIn(TypeRef nestedId);
            IOptional WithIsFramework(bool isFramework);
            TypeRef Build();
        }
    }
}
