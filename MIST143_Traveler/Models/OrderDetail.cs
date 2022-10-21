using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int TravelProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int? CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Order Order { get; set; }
        public virtual TravelProduct TravelProduct { get; set; }
    }
}
