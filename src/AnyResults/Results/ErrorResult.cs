namespace AnyResults.Results;

public record ErrorResult(string message, int code) : IErrorResult;