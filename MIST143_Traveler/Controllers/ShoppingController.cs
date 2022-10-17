using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
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
        public IActionResult testPage()
        {
            CPhotoListViewModel vmp = new CPhotoListViewModel();
            var photos = pt.TravelPictures.Where(x => x.TravelPictureId == 1).FirstOrDefault();
            vmp.proctphotos.Add(photos);
            return View(vmp);
        }
        public IActionResult List(int? TravelProductId)
        {

                var data = pt.TravelProducts.Where(x => x.TravelProductId == (int)TravelProductId).FirstOrDefault();
                return View(data);

        }
        public IActionResult PayData(int? TravelProductId)
        {
            var data = pt.TravelProducts.Where(x => x.TravelProductId == (int)TravelProductId).FirstOrDefault();
            return View(data);
        }
        public IActionResult PayCheckout(int? TravelProductId)
        {
            //PlanetTravelContext pt = new PlanetTravelContext();
            //var data = from p in pt.TravelProducts
            //        select p;
            var data = pt.TravelProducts.Where(x => x.TravelProductId == (int)TravelProductId).FirstOrDefault();
            return View(data);
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
