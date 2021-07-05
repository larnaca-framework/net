using System.Collections.Generic;

namespace LCA.Blueprints
{
    public class InvocationRequest<TRequest>
    {
#pragma warning disable 8618
        // maybe we can delete this constructor and convert the class into an interface
        public InvocationRequest() { }
#pragma warning restore 8618
        public InvocationRequest(
            string service,
            string endpoint,
            TRequest request,
            Dictionary<string, string>? metadata = null,
            ContextDetails? invokerContext = null,
            ServiceDetails? invokerService = null)
        {
            Service = service;
            Endpoint = endpoint;
            Request = request;
            Metadata = metadata;
            InvokerContext = invokerContext;
            InvokerService = invokerService;
        }
        public string Service { get; set; }
        public string Endpoint { get; set; }
        public TRequest Request { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }
        public ContextDetails? InvokerContext { get; set; }
        public ServiceDetails? InvokerService { get; set; }
    }
}