using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CCouponCMid
    {
      public List<CouponList> 已領過
        {
            get;set;
        }
       public  List<Coupon> 所有
        {
            get;set;
        }
    }
}
