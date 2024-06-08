﻿namespace Mango.Web.Models
{
    public class CouponDto
    {
        public Guid CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountedAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
