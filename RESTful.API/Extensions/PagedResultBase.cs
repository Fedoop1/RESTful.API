namespace RESTful.API.Extensions;

public class PagedResultBase<TEntity> : IPagedResult<TEntity>
{
    public IEnumerable<TEntity> Entities { get; init; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
