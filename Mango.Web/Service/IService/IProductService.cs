using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductAsync(Guid productId);
        Task<ResponseDto?> GetProductsAsync();
        Task<ResponseDto?> CresteProductAsync(ProductDto product);
        Task<ResponseDto?> UpdateProductAsync(ProductDto product);
        Task<ResponseDto?> DeleteProductAsync(Guid productId);
    }
}
