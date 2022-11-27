using System.ComponentModel.DataAnnotations;

namespace RESTful.API.Models;

public sealed class CategoryDetails
{
    [Required]
    public string Name { get; set; }
}
