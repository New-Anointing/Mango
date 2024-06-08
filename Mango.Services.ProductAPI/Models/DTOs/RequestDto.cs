using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models.DTOs
{
    public class RequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
