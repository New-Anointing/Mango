using Mango.Web.Models;
using Mango.Web.Models.Utility;
using Mango.Web.Service.IService;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        private static readonly string baseUrl = $"{SD.ProductAPIBase}/api/product";
        public async Task<ResponseDto?> CresteProductAsync(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = baseUrl,
                Data = product,
            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType= SD.ApiType.DELETE,
                Url = $"{baseUrl}/{productId}",
            });
        }

        public async Task<ResponseDto?> GetProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = baseUrl,
            });
        }

        public async Task<ResponseDto?> GetProductAsync(Guid productId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = $"{baseUrl}/{productId}",
            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.PUT,
                Url = baseUrl,
                Data = product,
            });
        }
    }
}
