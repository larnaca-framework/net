using System.Collections.Generic;

namespace LCA.Blueprints
{
    public class InvocationResponse<TResponse> : IResult<TResponse>
    {
#pragma warning disable 8618
        // maybe we can delete this constructor and convert the class into an interface
        public InvocationResponse() { }
#pragma warning restore 8618

        public InvocationResponse(int code,
            string message,
            TResponse? response,
            Dictionary<string, string>? metadata = null,
            ContextDetails? invokedContext = null,
            ServiceDetails? invokedService = null)
        {
            this.Code = code;
            this.Message = message;
            this.Response = response;
            this.Metadata = metadata;
            this.InvokedContext = invokedContext;
            this.InvokedService = invokedService;

        }
        public int Code { get; set; }
        public string? Message { get; set; }
        public TResponse? Response { get; set; }
        TResponse? IResult<TResponse>.Value { get => Response; set => Response = value; }
        public Dictionary<string, string>? Metadata { get; set; }
        public ContextDetails? InvokedContext { get; set; }
        public ServiceDetails? InvokedService { get; set; }
    }
}
