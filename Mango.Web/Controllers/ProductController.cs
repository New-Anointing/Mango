using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();
            ResponseDto? response = await _productService.GetProductsAsync();
            if (response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CresteProductAsync(model);
                if (response is not null && response.IsSuccess)
                {
                    TempData["Success"] = "Product Created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;

                }
            }
            return View(model);

        }

        public async Task<IActionResult> ProductEdit(Guid Id)
        {
            var product = await GetProduct(Id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.UpdateProductAsync(model);
                if (response is not null && response.IsSuccess)
                {
                    TempData["Success"] = "Product Edited successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(Guid Id)
        {
            var product = await GetProduct(Id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            ResponseDto? response = await _productService.DeleteProductAsync(model.ProductId);
            if(response is not null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(model);
        }


        private async Task<ProductDto> GetProduct(Guid Id)
        {
            ProductDto? product = new();
            ResponseDto? response = await _productService.GetProductAsync(Id);
            if (response is not null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return product;
        }

    }

}
