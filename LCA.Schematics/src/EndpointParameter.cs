namespace LCA.Schematics
{
    public class EndpointParameter
    {
        public EndpointParameter(string name, TypeRef type)
        {
            this.Name = name;
            this.Type = type;

        }
        public string Name { get; set; }
        public TypeRef Type { get; set; }
    }
}
