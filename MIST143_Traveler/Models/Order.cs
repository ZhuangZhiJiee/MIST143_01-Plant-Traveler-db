using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public int MemberId { get; set; }
        public string OrderDate { get; set; }
        public int OrderStatusId { get; set; }

        public virtual Member Member { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
