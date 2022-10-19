using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            Orders = new HashSet<Order>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public decimal Discount { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
