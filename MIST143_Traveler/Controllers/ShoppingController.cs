using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;



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
            return View();
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
        [HttpPost]
        public IActionResult PayData(CPayDataParameterViewModel p)
        {
            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
    
            CProductMemberViewModel pmv = new CProductMemberViewModel();

            pmv.產品會員 = (from c in pt.TravelProducts
                        where c.TravelProductId == p.TravelProductId
                        select new 產品會員
                        {
                            MemberName = v.MemberName,
                            Email = v.Email,
                            Phone = v.Phone,
                            TravelProductId = c.TravelProductId,
                            TravelProductName = c.TravelProductName,
                            Price = c.Price,
                            productpictures = c.TravelPictures.ToList(),
                        }).ToList();

            return View(pmv);

        }
        public IActionResult PayCheckout(int? TravelProductId)
        {
            var data = pt.TravelProducts.Where(x => x.TravelProductId == (int)TravelProductId).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult PayCheckout()
        {
            return View();
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
