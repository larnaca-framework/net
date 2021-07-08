using System.Collections.Generic;

namespace LCA.Schematics
{
    /// <summary>
    /// A collection of inter-referenced or just related schematics
    /// </summary>
    public class Model
    {
        public Model() : this(new List<TypeOutline>(), new List<Microservice>())
        { }
        public Model(List<TypeOutline> outlines, List<Microservice> microservices)
        {
            Outlines = outlines;
            Microservices = microservices;
        }
        public List<TypeOutline> Outlines { get; set; }
        public List<Microservice> Microservices { get; set; }
    }
}
