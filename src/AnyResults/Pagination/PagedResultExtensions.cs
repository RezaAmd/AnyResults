namespace AnyResults.Pagination;

public static class PagedResultExtensions
{
    public static PagedResult<TDestination> CastItemsTo<TSource, TDestination>(this PagedResult<TSource> pagedResult, Func<TSource, TDestination> func)
    {
        // mapping select
        var mappedItems = pagedResult.Items
            .Select(func)
            .ToList();

        // create instance with new items.
        return new PagedResult<TDestination>(
            mappedItems,
            pagedResult.Page,
            pagedResult.PageSize,
            pagedResult.TotalCount,
            pagedResult.TotalPages,
            pagedResult.HasPrevious,
            pagedResult.HasNext
        );
    }
}