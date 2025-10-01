namespace AnyResults.Results;

public static class ResultExtensions
{
    public static Result WithMessage(this Result result, string message)
    {
        result.Messages.Add(message);
        return result;
    }
    public static Result<TData> WithMessage<TData>(this Result<TData> result, string message)
    {
        result.Messages.Add(message);
        return result;
    }

    public static Result WithError(this Result result, ErrorResult error)
    {
        result.Errors.Add(error);
        return result;
    }
    public static Result<TData> WithError<TData>(this Result<TData> result, ErrorResult error)
    {
        result.Errors.Add(error);
        return result;
    }
}