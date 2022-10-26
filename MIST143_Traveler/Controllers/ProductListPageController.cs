using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using MIST143_Traveler.ShoppingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{

    public class ProductListPageController : Controller
    {
        private PlanetTravelContext pt;
        public ProductListPageController(PlanetTravelContext q)
        {
            pt = q;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductListHomePage()
        {
            
            return View();
        }
        public IActionResult test()
        {
            Ctestlass cv = new Ctestlass
            {
                productpicture = pt.TravelPictures.Where(c=>c.TravelPictureId==5).Select(p=>p.TravelPicture1).ToList(),
            };
            return View(cv);
        }
    }
}
