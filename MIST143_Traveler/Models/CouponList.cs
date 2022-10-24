using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class CouponList
    {
        public int CouponListId { get; set; }
        public int CouponId { get; set; }
        public int MembersId { get; set; }
        public bool CouponStatus { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Member Members { get; set; }
    }
}
