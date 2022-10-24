using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class 會員中心檢視優惠券
    {
        public List<我的優惠券> 優惠券列表;
    }
    public class 我的優惠券
    {
        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public decimal Discount { get; set; }
        public string ExDate { get; set; }
        public string Condition { get; set; }
    }
}
