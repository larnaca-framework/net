using System.Threading.Tasks;

namespace LCA.Blueprints
{
    public interface IStorer
    {
        Task<Result> Save<T>(string storeName, string key, T value);
        Task<Result> BulkSave<T>(string storeName, (string key, T value)[] data);
        Task<Result<T>> Get<T>(string storeName, string key);
        Task<Result<T[]>> BulkGet<T>(string storeName, string[] keys);
    }
}
