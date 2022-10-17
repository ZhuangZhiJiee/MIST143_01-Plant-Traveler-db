using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class ProductListPage2Controller : Controller
    {     
        private readonly PlanetTravelContext _planet;
        public ProductListPage2Controller(PlanetTravelContext planet)
        {
            _planet=planet;
        }
        public IActionResult Index()
        {
         
            return View();
        }
        public IActionResult ProductListPage()
        {
          var q = from p in _planet.TravelProducts
                    select p;
            return View(q.ToList());
        }
    }
}
