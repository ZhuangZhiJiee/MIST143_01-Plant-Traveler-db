using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponLists = new HashSet<CouponList>();
            Orders = new HashSet<Order>();
        }

        public int CouponId { get; set; }
        public string GiftKey { get; set; }
        public string CouponName { get; set; }
        public decimal Discount { get; set; }
        public string ExDate { get; set; }
        public string Condition { get; set; }
        public bool Useful { get; set; }

        public virtual ICollection<CouponList> CouponLists { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
