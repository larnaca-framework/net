using System.Threading.Tasks;

namespace LCA.Blueprints
{
    public struct Result : IResult
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }

    public struct Result<T> : IResult<T>
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
