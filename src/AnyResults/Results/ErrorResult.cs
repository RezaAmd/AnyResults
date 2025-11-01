namespace AnyResults.Results;

public record ErrorResult(string message, string code) : IErrorResult;