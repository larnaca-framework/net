namespace LCA.Blueprints
{
    public class ServiceDetails
    {

#pragma warning disable 8618
        // maybe we can delete this constructor and convert the class into an interface
        public ServiceDetails() { }
#pragma warning restore 8618
        public ServiceDetails(string name, string @namespace)
        {
            Name = name;
            Namespace = @namespace;
        }
        public string Name { get; set; }
        public string Namespace { get; set; }
    }
}
