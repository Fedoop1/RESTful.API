using RESTful.API.Models;

namespace RESTful.API.Data;

public sealed class ItemEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}
