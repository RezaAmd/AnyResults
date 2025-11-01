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
    public static Result<TData> Fail<TData>(List<string>? messages = null, TData? data = default)
    => new Result<TData>(false, data, messages);
    public static Result<TData> Fail<TData>(string? error = null, TData? data = default)
        => new Result<TData>(false, data, [error!]);

    /// <summary>
    /// Map extension message to result message.
    /// </summary>
    public static Result Fail(Exception exception)
        => new(false, [exception.Message]);

    /// <summary>
    /// Map extension message to result message.
    /// </summary>
    public static Result<TData> Fail<TData>(Exception exception, TData data)
        => new Result<TData>(false, data, [exception.Message]);
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