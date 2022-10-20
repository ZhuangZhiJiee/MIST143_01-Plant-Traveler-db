using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MIST143_Traveler.MViewModel;

namespace MIST143_Traveler.Controllers
{
    public class TransportationPageController : Controller
        
    {
        private PlanetTravelContext ptc;

        public TransportationPageController(PlanetTravelContext q)
        {
            ptc = q;
        }

        public IActionResult Index()
        {
            var datas = from p in ptc.TravelProducts
                        where p.Country.CountryName=="台灣"
                        select p;
            return View(datas);
        }
        public IActionResult TransportationHomePage()
        {
            return View();
            //1234
        }
        [HttpPost]
        public IActionResult TransportationHomePage(CLocationKeyWordViewModel 參數名稱)
        {
            return RedirectToAction("showTransportationHomePage", 參數名稱);
            //1234
        }
        public IActionResult showTransportationHomePage(CLocationKeyWordViewModel 傳入的資料)
        {
            return View(傳入的資料);
        }
    }
}
