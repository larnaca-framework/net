namespace LCA.Schematics
{
    /// <summary>
    /// Identical to the ETypeKind with the addition of the GenericParameter.
    /// A GenericParameters does not have an outline
    /// </summary>
    public enum ETypeRefKind
    {
        Class = 0,
        Struct = 1,
        Enum = 2,
        Primitive = 3,
        Interface = 4,
        GenericParameter = 5
    }
}