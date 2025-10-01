using System.Collections.Generic;

namespace AnyResults.Pagination;

public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; private set; }
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public long TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPrevious { get; private set; }
    public bool HasNext { get; private set; }

    public PagedResult(
        IReadOnlyList<T> items,
        int page,
        int pageSize,
        long totalCount,
        int totalPages,
        bool hasPrevious,
        bool hasNext)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = totalPages;
        HasPrevious = hasPrevious;
        HasNext = hasNext;
    }

    public static PagedResult<T> Empty(int page, int pageSize) =>
        new PagedResult<T>(
            new List<T>(),
            page,
            pageSize,
            0,
            0,
            false,
            false
        );
}