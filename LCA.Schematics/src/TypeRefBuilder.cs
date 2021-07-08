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
        ETypeRefKind _kind;

        // will be initialized by the builder in the WithName(...) method
#nullable disable
        string _name;
#nullable enable
        bool _isFramework;
        string? _namespace;
        TypeRef? _nestedIn;
        int[]? _arrayDimensions;
        TypeRef[]? _genericArguments;
        public static IStart Start() => new TypeRefBuilder();
        public IOptional From(TypeRef typeRef)
        {
            _kind = typeRef.Kind;
            _name = typeRef.Name;
            _isFramework = typeRef.IsFramework;
            _namespace = typeRef.Namespace;
            _nestedIn = typeRef.NestedIn?.Clone();
            _arrayDimensions = typeRef.ArrayDimensions;
            _genericArguments = typeRef.GenericArguments?.Select(a => a.Clone()).ToArray();
            return this;
        }
        IName IStart.WithKind(ETypeRefKind kind)
        {
            _kind = kind;
            return this;
        }
        IOptionalWithNamespace IName.WithName(string name)
        {
            _name = name;
            return this;
        }
        IOptional IName.WithName(string @namespace, string name)
        {
            _namespace = @namespace;
            _name = name;
            return this;
        }

        IOptional IOptionalWithNamespace.WithNamespace(string @namespace)
        {
            _namespace = @namespace;
            return this;
        }
        IOptional IOptional.WithArrayDimensions(int[] dimensions) => (IOptional)(this as IOptionalWithNamespace).WithArrayDimensions(dimensions);
        IOptionalWithNamespace IOptionalWithNamespace.WithArrayDimensions(int[] dimensions)
        {
            _arrayDimensions = _arrayDimensions?.Concat(dimensions) ?? dimensions;
            return this;
        }
        IOptional IOptional.WithGenericArguments(params TypeRef[] arguments) => (IOptional)(this as IOptionalWithNamespace).WithGenericArguments(arguments);
        IOptionalWithNamespace IOptionalWithNamespace.WithGenericArguments(params TypeRef[] arguments)
        {
            _genericArguments = _genericArguments?.Concat(arguments) ?? arguments;
            return this;
        }
        IOptional IOptional.WithGenericParameters(params string[] parameters) => (IOptional)(this as IOptionalWithNamespace).WithGenericParameters(parameters);
        IOptionalWithNamespace IOptionalWithNamespace.WithGenericParameters(params string[] parameters)
        {
            var typedParameters = parameters.Select(p => new TypeRef(ETypeRefKind.GenericParameter, p, false)).ToArray();
            _genericArguments = _genericArguments?.Concat(typedParameters) ?? typedParameters;
            return this;
        }
        IOptional IOptional.WithNestedIn(TypeRef nestedId) => (IOptional)(this as IOptionalWithNamespace).WithNestedIn(nestedId);
        IOptionalWithNamespace IOptionalWithNamespace.WithNestedIn(TypeRef nestedId)
        {
            _nestedIn = nestedId;
            return this;
        }
        TypeRef IOptional.Build() => (this as IOptionalWithNamespace).Build();
        TypeRef IOptionalWithNamespace.Build() => new TypeRef(
                _kind,
                _name,
                _isFramework,
                _namespace,
                _nestedIn,
                _arrayDimensions,
                _genericArguments
            );
        public interface IStart
        {
            IOptional From(TypeRef typeRef);
            IName WithKind(ETypeRefKind kind);
        }
        public interface IName
        {
            IOptionalWithNamespace WithName(string name);
            IOptional WithName(string @namespace, string name);
        }
        public interface IOptionalWithNamespace
        {
            IOptional WithNamespace(string @namespace);
            IOptionalWithNamespace WithArrayDimensions(int[] dimensions);
            IOptionalWithNamespace WithGenericArguments(params TypeRef[] arguments);
            IOptionalWithNamespace WithGenericParameters(params string[] parameters);
            IOptionalWithNamespace WithNestedIn(TypeRef nestedId);
            TypeRef Build();
        }
        public interface IOptional
        {
            IOptional WithArrayDimensions(int[] dimensions);
            IOptional WithGenericArguments(params TypeRef[] arguments);
            IOptional WithGenericParameters(params string[] parameters);
            IOptional WithNestedIn(TypeRef nestedId);
            TypeRef Build();
        }
    }
}
