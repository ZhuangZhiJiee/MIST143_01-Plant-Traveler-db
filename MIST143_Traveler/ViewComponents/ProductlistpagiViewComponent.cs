using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ViewComponents
{
    public class ProductlistpagiViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly PlanetTravelContext _travel;
        public ProductlistpagiViewComponent(PlanetTravelContext travel) 
        {
            _travel = travel;
        }

        public async Task<IViewComponentResult> InvokeAsync(TravelProduct tr)
        {

            return View(tr);
        }
    }
}
