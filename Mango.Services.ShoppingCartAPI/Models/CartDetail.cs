using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Mango.Services.ShoppingCartAPI.Models.DTOs;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartDetail
    {
        [Key]
        public Guid CartDetailsId { get; set; } = Guid.NewGuid();
        public virtual CartHeader CartHeader { get; set; }
        public Guid ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
