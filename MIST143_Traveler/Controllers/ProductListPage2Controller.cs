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
            _planet = planet;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult ProductListPage(string keyword)
        {
            ////  string keyword = Request.Form["txtkeyword"];
            //  IEnumerable<TravelProduct> datas = null;
            //  if (string.IsNullOrEmpty(keyword))
            //  {
            //      keyword= "ProductListPage";
            //      return View(keyword);
            //  }
            //  else
            //  {
            //      datas =( from i in _planet.TravelProducts
            //              where i.TravelProductId == Convert.ToInt32(keyword) || i.TravelProductName.Contains(keyword) || i.Description.Contains(keyword)
            //              select i).ToList();
            var q = from p in _planet.TravelProducts
                    where p.TravelProductName.Contains(keyword) || p.Description.Contains(keyword)
                    select p;
            return View(q.ToList());

        }
        public IActionResult filter(string keyword)
        {
            var q = from p in _planet.TravelProducts
                    where p.TravelProductName.Contains(keyword) || p.Description.Contains(keyword)
                    select p;
            if (q.Count() <= 0)
            {
                q = from p in _planet.TravelProducts
                    select p;
            }
            ViewBag.Count = q.Count();
            //return View();
            return ViewComponent("Productlistpagi", q.ToList());
        }
        public IActionResult selecter()
        {
            return View();
        }
    }
}
