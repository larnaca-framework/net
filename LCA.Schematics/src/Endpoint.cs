namespace LCA.Schematics
{
    public class Endpoint
    {
        public Endpoint(string name)
        {
            this.Name = name;

        }
        public string Name { get; set; }
        public EndpointParameter[]? RequestParameters { get; set; }
        public TypeRef? Response { get; set; }
    }
}
