using Mango.Services.ShoppingCartAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
	public class CartDetail
	{
		[Key]
		public int CartDetailsId { get; set; }
		public CartHeader CartHeader { get; set; }
		public Guid ProductId { get; set; }
		[NotMapped]
		public ProductDto Product { get; set; }
		public int Count { get; set; }

	}
}
