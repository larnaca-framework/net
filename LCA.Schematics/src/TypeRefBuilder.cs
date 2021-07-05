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
        private TypeRef typeRef;
        public static IStart Start() => new TypeRefBuilder();
        public IOptional From(TypeRef typeRef)
        {
            this.typeRef = typeRef.Clone();
            return this;
        }
        IName IStart.WithKind(ETypeRefKind kind)
        {
            typeRef = new TypeRef()
            {
                Kind = kind
            };
            return this;
        }
        IOptionalWithNamespace IName.WithName(string name)
        {
            typeRef.Name = name;
            return this;
        }
        IOptional IName.WithName(string @namespace, string name)
        {
            typeRef.Namespace = @namespace;
            typeRef.Name = name;
            return this;
        }

        IOptional IOptionalWithNamespace.WithNamespace(string @namespace)
        {
            typeRef.Namespace = @namespace;
            return this;
        }
        IOptional IOptional.WithArrayDimensions(int[] dimensions) => (IOptional)(this as IOptionalWithNamespace).WithArrayDimensions(dimensions);
        IOptionalWithNamespace IOptionalWithNamespace.WithArrayDimensions(int[] dimensions)
        {
            typeRef.ArrayDimensions = typeRef.ArrayDimensions?.Concat(dimensions) ?? dimensions;
            return this;
        }
        IOptional IOptional.WithGenericArguments(params TypeRef[] arguments) => (IOptional)(this as IOptionalWithNamespace).WithGenericArguments(arguments);
        IOptionalWithNamespace IOptionalWithNamespace.WithGenericArguments(params TypeRef[] arguments)
        {
            typeRef.GenericArguments = typeRef.GenericArguments?.Concat(arguments) ?? arguments;
            return this;
        }
        IOptional IOptional.WithGenericParameters(params string[] parameters) => (IOptional)(this as IOptionalWithNamespace).WithGenericParameters(parameters);
        IOptionalWithNamespace IOptionalWithNamespace.WithGenericParameters(params string[] parameters)
        {
            var typedParameters = parameters.Select(p => new TypeRef()
            {
                Kind = ETypeRefKind.GenericParameter,
                Name = p,
            }).ToArray();
            typeRef.GenericArguments = typeRef.GenericArguments?.Concat(typedParameters) ?? typedParameters;
            return this;
        }
        IOptional IOptional.WithNestedIn(TypeRef nestedId) => (IOptional)(this as IOptionalWithNamespace).WithNestedIn(nestedId);
        IOptionalWithNamespace IOptionalWithNamespace.WithNestedIn(TypeRef nestedId)
        {
            typeRef.NestedIn = nestedId;
            return this;
        }
        TypeRef IOptional.Build() => (this as IOptionalWithNamespace).Build();
        TypeRef IOptionalWithNamespace.Build()
        {
            return typeRef;
        }
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
