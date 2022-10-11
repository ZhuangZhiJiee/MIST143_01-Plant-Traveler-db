using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Payment
    {
        public Payment()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int PaymentId { get; set; }
        public string PaymentName { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
