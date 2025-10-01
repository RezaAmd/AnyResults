namespace AnyResults.Samples.Models.Todo;

public record TodoViewModel(
    Guid Id,
    string Title,
    DateTimeOffset? CompletedOn,
    DateTimeOffset CreatedOn
    );