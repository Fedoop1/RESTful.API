using RESTful.API.Extensions;

namespace RESTful.API.Queries;

public class ItemsPagedQuery : PagedQueryBase
{
    public int? CategoryId { get; set; }
}
