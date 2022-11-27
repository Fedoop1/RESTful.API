namespace RESTful.API.Extensions
{
    public interface IPagedQuery
    {
        int PageSize { get; set; }
        int Page { get; set; }
    }
}
