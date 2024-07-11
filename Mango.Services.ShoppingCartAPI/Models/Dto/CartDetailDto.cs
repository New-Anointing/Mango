using Mango.Services.ShoppingCartAPI.Models.DTOs;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDetailDto
    {
        public Guid CartDetailsId { get; set; }
        public CartHeader? CartHeader { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
