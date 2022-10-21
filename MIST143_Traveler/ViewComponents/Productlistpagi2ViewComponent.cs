using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ViewComponents
{
    public class Productlistpagi2ViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public readonly PlanetTravelContext _planet;
        public Productlistpagi2ViewComponent(PlanetTravelContext planet)
    {
        _planet = planet;
    }
        public async Task<IViewComponentResult> InvokeAsync(List<TravelProduct> pr)
        {

            return View(pr);
        }
    }
}
