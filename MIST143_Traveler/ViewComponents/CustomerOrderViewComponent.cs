using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ViewComponents
{
    [Microsoft.AspNetCore.Mvc.ViewComponent]
    public class CustomerOrderViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        //private readonly PlanetTravelContext _PlanetTravelContext;
        //public CustomerOrderViewComponent(PlanetTravelContext PlanetTrave)
        //{
        //    _PlanetTravelContext = PlanetTrave;
        //}
        public async Task<IViewComponentResult> InvokeAsync(COrderDetail Oe)
        {
            
            return View(Oe);
        }
    }
}
