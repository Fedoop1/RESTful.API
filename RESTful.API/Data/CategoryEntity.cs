namespace RESTful.API.Data;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ItemEntity> Items { get; set; }
}
