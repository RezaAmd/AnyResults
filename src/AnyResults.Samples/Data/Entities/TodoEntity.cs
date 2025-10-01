namespace AnyResults.Samples.Data.Entities;

public class TodoEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; set; }
    public DateTimeOffset? CompletedOn { get; set; }
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;

    #region Methods
    public void Complete()
    {
        if (CompletedOn == null)
            return;

        CompletedOn = DateTimeOffset.UtcNow;
    }
    #endregion
}