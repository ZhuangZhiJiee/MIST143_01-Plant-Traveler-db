using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ViewComponents
{
    [Microsoft.AspNetCore.Mvc.ViewComponent]
    public class CouponExpViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<我的優惠券> ex)
        {
            return View(ex);

        }
    }
}
