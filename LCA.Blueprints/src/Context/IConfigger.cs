using System.Threading.Tasks;

namespace LCA.Blueprints
{
    public interface IConfigger
    {
        Result<T> GetConfig<T>(string key);
        Result<T> GetSecret<T>(string key);
        Task<Result<T>> GetConfigAsync<T>(string key);
        Task<Result<T>> GetSecretAsync<T>(string key);
    }
}
