using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        public ProductApiController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _context.Products.ToList();
                _response.Data = _mapper.Map<IEnumerable<ProductDto>>(objList);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }
        [HttpGet]
        [Route("{ProductId:guid}")]
        public ResponseDto Get(Guid ProductId)
        {
            try
            {
                Product obj = _context.Products.FirstOrDefault(x => x.ProductId == ProductId);
                if (obj == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product with specified id does not exist";
                    _response.Data = _mapper.Map<ProductDto>(obj);
                    return _response;
                }
                _response.Data = _mapper.Map<ProductDto>(obj);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] RequestDto obj)
        {
            try
            {
                var product = _mapper.Map<Product>(obj);
                _context.Products.Add(product);
                _context.SaveChanges();
                _response.Data = _mapper.Map<ProductDto>(product);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto obj)
        {
            try
            {
                var product = _mapper.Map<Product>(obj);
                _context.Products.Update(product);
                _context.SaveChanges();
                _response.Data = _mapper.Map<ProductDto>(product);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(Guid id)
        {
            try
            {
                Product productToDelete = _context.Products.First(x => x.ProductId == id);
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
            return _response;
        }
    }
}
