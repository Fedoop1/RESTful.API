using System.ComponentModel.DataAnnotations;

namespace RESTful.API.Models;

public sealed class Item
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int CategoryId { get; init; }
}
