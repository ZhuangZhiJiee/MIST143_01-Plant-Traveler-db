using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ViewComponents
{
    [Microsoft.AspNetCore.Mvc.ViewComponent]
    public class CouponAllViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(會員中心檢視優惠券 Coup)
        {
            return View(Coup);
        }
    }
}
