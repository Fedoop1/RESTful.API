namespace RESTful.API.Extensions;

public interface IPagedResult<TEntity>
{
    IEnumerable<TEntity> Entities { get; init; }
    int Page { get; set; }
    int PageSize { get; set; }
    int TotalPages { get; set; }
}
