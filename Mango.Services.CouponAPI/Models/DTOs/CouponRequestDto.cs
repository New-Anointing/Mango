namespace Mango.Services.CouponAPI.Models.DTOs
{
    public class CouponRequestDto
    {
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountedAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
