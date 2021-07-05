namespace LCA.Blueprints
{
    public interface IResult
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess() => Code == 0;
        public bool IsFail() => !IsSuccess();
        public bool Success { get => IsSuccess(); }
        public bool Fail { get => !IsSuccess(); }
    }
    public interface IResult<T> : IResult
    {
        public T? Value { get; set; }
    }
}