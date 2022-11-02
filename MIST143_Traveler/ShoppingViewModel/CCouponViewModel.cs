using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CCouponViewModel
    {
        public int CouponId { get; set; }
        public int CouponListId { get; set; }
        public string CouponName { get; set; }
        public decimal Discount { get; set; }
        public string Condition { get; set; }
        public string ExDate { get; set; }
    }
}
