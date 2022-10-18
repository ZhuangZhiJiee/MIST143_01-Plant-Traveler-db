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
            //CProductViewModel vmp = new CProductViewModel();
            //vmp.productpictures = pt.TravelPictures.Where(x => x.TravelProductId == 9).ToList();

            //return View(vmp);
            CProductxViewModel vmp = new CProductxViewModel();

            vmp.產品列表 = (from c in pt.TravelProducts
                        where c.TravelProductId == 9
                        select new 產品格式
                        {
                            TravelProductName = c.TravelProductName,
                            TravelProductId = c.TravelProductId,
                            Price = c.Price,
                            TravelProductTypeId = c.TravelProductTypeId,
                            Stocks = c.Stocks,
                            Description = c.Description,
                            CountryId = c.CountryId,
                            Cost = c.Cost,
                            EventIntroduction = c.EventIntroduction,
                            PreparationDescription = c.PreparationDescription,
                            productpictures = c.TravelPictures.ToList(),
                        }).ToList();

            return View(vmp);

        }
        public IActionResult List(int? TravelProductId)
        {
            CProductxViewModel vmp = new CProductxViewModel();

            vmp.產品列表 = (from c in pt.TravelProducts
                           where c.TravelProductId == (int)TravelProductId
                        select new 產品格式
                           {
                               TravelProductName = c.TravelProductName,
                               TravelProductId = c.TravelProductId,
                               Price = c.Price,
                               TravelProductTypeId = c.TravelProductTypeId,
                               Stocks = c.Stocks,
                               Description = c.Description,
                               CountryId = c.CountryId,
                               Cost = c.Cost,
                               EventIntroduction = c.EventIntroduction,
                               MapUrl = c.MapUrl,
                               PreparationDescription = c.PreparationDescription,
                               productpictures = c.TravelPictures.ToList(),
                           }).ToList();

            return View(vmp);

        }
        public IActionResult PayData(int? TravelProductId)
        {
            CProductxViewModel vmp = new CProductxViewModel();

            vmp.產品列表 = (from c in pt.TravelProducts
                        where c.TravelProductId == (int)TravelProductId
                        select new 產品格式
                        {
                            TravelProductName = c.TravelProductName,
                            TravelProductId = c.TravelProductId,
                            Price = c.Price,
                            TravelProductTypeId = c.TravelProductTypeId,
                            Stocks = c.Stocks,
                            Description = c.Description,
                            CountryId = c.CountryId,
                            Cost = c.Cost,
                            EventIntroduction = c.EventIntroduction,
                            MapUrl = c.MapUrl,
                            PreparationDescription = c.PreparationDescription,
                            productpictures = c.TravelPictures.ToList(),
                        }).ToList();

            return View(vmp);

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
