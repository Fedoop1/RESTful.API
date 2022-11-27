namespace RESTful.API.Extensions;

public class PagedQueryBase : IPagedQuery
{
    public int PageSize { get; set; } = 50;
    public int Page { get; set; } = 1;
}
