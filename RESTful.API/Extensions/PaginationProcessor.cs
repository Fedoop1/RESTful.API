namespace RESTful.API.Extensions;

public static class PaginationProcessor
{
    public static PagedResultBase<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> source, IPagedQuery query)
    {
        var items = source.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);
        var totalPages = (int)Math.Ceiling((decimal) source.Count() / query.PageSize);

        return new PagedResultBase<TEntity>
        {
            Entities = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = totalPages
        };
    }
}
