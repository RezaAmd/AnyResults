using System.Collections.Generic;

namespace AnyResults.Results;

public static class ResultExtensions
{
    /// <summary>
    /// Add a new message to result.
    /// </summary>
    public static Result WithMessage(this Result result, string message)
    {
        result.Messages.Add(message);
        return result;
    }
    /// <summary>
    /// Add a new message to result.
    /// </summary>
    public static Result<TData> WithMessage<TData>(this Result<TData> result, string message)
    {
        result.Messages.Add(message);
        return result;
    }
    /// <summary>
    /// Add bulk message to result.
    /// </summary>
    public static Result WithMessage(this Result result, List<string> message)
    {
        result.Messages.AddRange(message);
        return result;
    }
    /// <summary>
    /// Add bulk message to result.
    /// </summary>
    public static Result<TData> WithMessage<TData>(this Result<TData> result, List<string> message)
    {
        result.Messages.AddRange(message);
        return result;
    }

    /// <summary>
    /// Add a new error message to result.
    /// </summary>
    public static Result WithError(this Result result, ErrorResult error)
    {
        result.Errors.Add(error);
        return result;
    }
    /// <summary>
    /// Add a new error message to result.
    /// </summary>
    public static Result<TData> WithError<TData>(this Result<TData> result, ErrorResult error)
    {
        result.Errors.Add(error);
        return result;
    }
    /// <summary>
    /// Add bulk error messages to result.
    /// </summary>
    public static Result WithError(this Result result, List<ErrorResult> error)
    {
        result.Errors.AddRange(error);
        return result;
    }
    /// <summary>
    /// Add bulk error messages to result.
    /// </summary>
    public static Result<TData> WithError<TData>(this Result<TData> result, List<ErrorResult> error)
    {
        result.Errors.AddRange(error);
        return result;
    }
}