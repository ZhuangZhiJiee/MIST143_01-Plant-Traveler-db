using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class ShoppingController : Controller
    {
        private PlanetTravelContext pt;
        public ShoppingController(PlanetTravelContext q) 
        {
            pt = q;
        }
        public IActionResult List()
        {
            var data = pt.TravelProducts.Where(x => x.TravelProductId == 1).FirstOrDefault();
            return View(data);
        }
        public IActionResult PayData()
        {
            var data = pt.TravelProducts.Where(x => x.TravelProductId == 1).FirstOrDefault();
            return View(data);
        }
        public IActionResult PayCheckout()
        {
            //PlanetTravelContext pt = new PlanetTravelContext();
            //var data = from p in pt.TravelProducts
            //        select p;
            var data = pt.TravelProducts.Where(x => x.TravelProductId == 1).FirstOrDefault();
            return View(data);
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
