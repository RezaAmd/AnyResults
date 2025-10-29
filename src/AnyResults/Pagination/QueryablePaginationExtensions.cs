using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AnyResults.Pagination;

public static class QueryablePaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PaginationQuery page,
        CancellationToken cancellationToken = default) where T : class
    {
        if (!string.IsNullOrWhiteSpace(page.Sort))
        {
            var sort = page.Sort.Trim();
            var descending = sort.StartsWith("-");
            var property = descending ? sort[1..] : sort;

            query = query.OrderByProperty(property, @descending);
        }

        query = query.AddDefaultOrderIfMissing();

        query = query.AsNoTracking();

        var total = await query.LongCountAsync(cancellationToken);

        var items = await query
            .Skip((page.Page - 1) * page.PageSize)
            .Take(page.PageSize)
            .ToListAsync(cancellationToken);

        var totalPages = total == 0 ? 0 : (int)Math.Ceiling((double)total / page.PageSize);

        return new PagedResult<T>(
            items: items,
            page: page.Page,
            pageSize: page.PageSize,
            totalCount: total,
            totalPages: totalPages,
            hasPrevious: page.Page > 1,
            hasNext: page.Page < totalPages
        );
    }

    public static async Task<PagedResult<TOut>> ToPagedResultAsync<T, TOut>(
        this IQueryable<T> query,
        PaginationQuery page,
        Expression<Func<T, TOut>> selector,
        CancellationToken cancellationToken = default) where T : class
    {
        if (!string.IsNullOrWhiteSpace(page.Sort))
        {
            var sort = page.Sort.Trim();
            var descending = sort.StartsWith("-");
            var property = descending ? sort[1..] : sort;

            query = query.OrderByProperty(property, @descending);
        }

        query = query.AddDefaultOrderIfMissing();

        query = query.AsNoTracking();

        var total = await query.LongCountAsync(cancellationToken);

        var projected = query.Select(selector);

        var items = await projected
            .Skip((page.Page - 1) * page.PageSize)
            .Take(page.PageSize)
            .ToListAsync(cancellationToken);

        var totalPages = total == 0 ? 0 : (int)Math.Ceiling((double)total / page.PageSize);

        return new PagedResult<TOut>(
            items: items,
            page: page.Page,
            pageSize: page.PageSize,
            totalCount: total,
            totalPages: totalPages,
            hasPrevious: page.Page > 1,
            hasNext: page.Page < totalPages
        );
    }

    private static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.PropertyOrField(param, propertyName);
        var keySel = Expression.Lambda(body, param);

        var method = descending ? "OrderByDescending" : "OrderBy";
        var call = Expression.Call(
            typeof(Queryable),
            method,
            [typeof(T), body.Type],
            source.Expression,
            Expression.Quote(keySel));

        return source.Provider.CreateQuery<T>(call);
    }

    private static bool HasOrderBy(Expression expr) =>
        expr is MethodCallExpression m &&
        m.Method.DeclaringType == typeof(Queryable) &&
        (m.Method.Name is "OrderBy" or "OrderByDescending" or "ThenBy" or "ThenByDescending"
         || HasOrderBy(m.Arguments[0]));

    private static IQueryable<T> AddDefaultOrderIfMissing<T>(this IQueryable<T> source)
    {
        if (!HasOrderBy(source.Expression))
        {
            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.PropertyOrField(param, "Id");
            var keySel = Expression.Lambda(body, param);

            var call = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                [typeof(T), body.Type],
                source.Expression,
                Expression.Quote(keySel));

            return source.Provider.CreateQuery<T>(call);
        }
        return source;
    }
}