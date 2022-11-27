using System.ComponentModel.DataAnnotations;

namespace RESTful.API.Models
{
    public sealed class ItemDetails
    {
        [Required]
        public string Name { get; init; }

        public int CategoryId { get; init; }
    }
}
