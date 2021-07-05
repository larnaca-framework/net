using System.Threading.Tasks;

namespace LCA.Blueprints
{
    public interface IContext
    {
        ILogger Logger { get; }
        IConfigger Configger { get; }
        IStorer Storer { get; }
        ContextDetails Details { get; }
        Task<Result<TResponse>> InvokeEndpoint<TRequest, TResponse>(string service, string endpoint, TRequest request);
        Task<InvocationResponse<TResponse>> InvokeEndpoint<TRequest, TResponse>(InvocationRequest<TRequest> request);
    }
}
