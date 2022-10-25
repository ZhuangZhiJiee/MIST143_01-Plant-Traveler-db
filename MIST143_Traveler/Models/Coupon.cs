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
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int CouponId { get; set; }
        public string GiftKey { get; set; }
        public string CouponName { get; set; }
        public decimal Discount { get; set; }
        public string ExDate { get; set; }
        public string Condition { get; set; }
        public string GetDate { get; set; }

        public virtual ICollection<CouponList> CouponLists { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
