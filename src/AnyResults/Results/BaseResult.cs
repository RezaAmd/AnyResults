using System.Collections.Generic;

namespace AnyResults.Results;

public class BaseResult
{
    public BaseResult(bool isSuccess, List<string>? messages = default)
    {
        IsSuccess = isSuccess;
        if (messages != null && messages.Count > 0)
            Messages = messages;
    }
    public bool IsSuccess { get; private set; }
    public List<string> Messages { get; private set; } = new();
    public List<ErrorResult> Errors { get; set; } = new();
}