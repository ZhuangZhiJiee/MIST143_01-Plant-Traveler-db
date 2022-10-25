using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using MIST143_Traveler.ShoppingViewModel;

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
            CProductViewModel vmp = new CProductViewModel();

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
                               //Runproductpictures = c.TravelPictures.Where(a => a.PictureStatus ==2).ToList(),
                               DailyDetailText = c.TravelProductDetails.FirstOrDefault().DailyDetailText,
                        }).ToList();

            return View(vmp);

        }
        [HttpPost]
        public IActionResult AddToSession(CAddToSessionViewModel s) 
        {
            string jsonBurchased = JsonSerializer.Serialize(s);
            HttpContext.Session.SetString(
                CDictionary.SK_PURCHASED_PRODUCT, jsonBurchased);
            return NoContent();
        }
        public IActionResult ShoppingCart()
        {
            var shopping = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
            var c = JsonSerializer.Deserialize<CshoppingCartViewModel>(shopping);
            CTranshoppingCartViewModel tsc = new CTranshoppingCartViewModel()
            {
                TravelProductId = c.TravelProductId,
                TravelProductName = pt.TravelProducts.Where(a => a.TravelProductId == c.TravelProductId).FirstOrDefault().TravelProductName,
                Count = c.Count,
                Price = pt.TravelProducts.Where(a => a.TravelProductId == c.TravelProductId).FirstOrDefault().Price,
                productpicture = pt.TravelPictures.Where(a => a.TravelProductId == c.TravelProductId).FirstOrDefault().TravelPicture1,
            };
            return View(tsc);
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
                            Count = p.Count,
                        }).ToList();
            return View(pmv);

        }
        [HttpPost]
        public IActionResult PayCheckout(CPayDataParameterViewModel p)
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
                            Count = p.Count,
                        }).ToList();
            return View(pmv);
        }
        [HttpPost]
        public IActionResult PayEnd(CPayViewModel p)
        {
            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
            Order od = new Order()
            {
                MembersId = v.MembersId,
                PaymentId = p.PaymethodId,
                OrderDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                OrderStatusId = 3,
            };
            pt.Orders.Add(od);
            pt.SaveChanges();

            OrderDetail odd = new OrderDetail()
            {
                OrderId = pt.Orders.OrderBy(e => e.OrderId).LastOrDefault().OrderId,
                TravelProductId = p.TravelProductId,
                UnitPrice = p.UnitPrice,
                Quantity =p.Quantity,
            };
            pt.OrderDetails.Add(odd);
            pt.SaveChanges();
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Index", "Home");
        }
    }
}
