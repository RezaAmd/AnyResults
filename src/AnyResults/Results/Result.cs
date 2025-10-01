using System.Collections.Generic;

namespace AnyResults.Results;

public class Result : BaseResult
{
    public Result(bool isSuccess, List<string>? messages = default)
        : base(isSuccess, messages) { }

    #region Methods
    public static Result Ok()
        => new(true, []);
    public static Result<TData> Ok<TData>(TData? data = default)
    => new Result<TData>(true, data, []);

    public static Result Fail(string message)
    => new(false, [message]);
    public static Result Fail(List<string>? messages = null)
    => new(false, messages);
    #endregion
}

public class Result<TData> : BaseResult
{
    public TData? Data { get; set; } = default;
    public Result(bool isSuccess, TData? data = default, List<string>? messages = default)
    : base(isSuccess, messages)
    {
        Data = data;
    }

    #region Implicit
    public static implicit operator Result(Result<TData> result)
        => new Result(result.IsSuccess, result.Messages);
    public static implicit operator Result<TData>(Result result)
        => new Result<TData>(result.IsSuccess, default, result.Messages);
    #endregion
}