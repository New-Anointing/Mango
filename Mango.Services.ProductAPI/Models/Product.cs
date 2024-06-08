using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models
{
    public class Product
    {
        public Guid ProductId { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 1000)]
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

    }
}
