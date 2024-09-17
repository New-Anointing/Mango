using Mango.Services.ShoppingCartAPI.Models.DTOs;

namespace Mango.Services.ShoppingCartAPI.Service
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> GetProducts();
	}
}
