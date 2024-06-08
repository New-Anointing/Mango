using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    public class Coupon
    {
        public Guid CouponId { get; set; } = Guid.NewGuid();
        [Required]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        public double DiscountedAmount { get; set; }
        public int MinAmount { get; set; } 
    }
}
