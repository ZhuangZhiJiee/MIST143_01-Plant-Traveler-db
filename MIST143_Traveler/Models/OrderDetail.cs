using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderDetailId { get; set; }
        public int TravelProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CouponId { get; set; }
        public int PaymentId { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual TravelProduct TravelProduct { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
