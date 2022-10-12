using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MIST143_Traveler.ViewComponent
{
    [Microsoft.AspNetCore.Mvc.ViewComponent]
    public class ProductViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
