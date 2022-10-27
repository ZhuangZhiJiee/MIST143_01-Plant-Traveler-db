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
        public IActionResult List(int? TravelProductId)
        {
            CProductViewModel prod = pt.TravelProducts.Where(p => p.TravelProductId == TravelProductId)
                .Select(s => new CProductViewModel
                {
                    TravelProductName = s.TravelProductName,
                    TravelProductId = s.TravelProductId,
                    Price = s.Price,
                    Stocks = s.Stocks,
                    Description = s.Description,
                    EventIntroduction = s.EventIntroduction,
                    MapUrl = s.MapUrl,
                    PreparationDescription = s.PreparationDescription,
                    productpictures = s.TravelPictures.ToList(),
                    Date = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.Date).ToList(),
                    HotelName = s.TravelProductDetails.Where(p => p.TravelProductId == s.TravelProductId).Select(p => p.Hotel.HotelName).ToList(),
                    DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                    _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                    {
                        ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                    }).ToList(),
                }).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult AddToSession(CAddToSessionViewModel s) 
        {
            string jsonCart = "";
            List<CshoppingCartViewModel> list = null;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
                list = new List<CshoppingCartViewModel>();
            else
            {
                jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                list = JsonSerializer.Deserialize<List<CshoppingCartViewModel>>(jsonCart);
            }
            CshoppingCartViewModel item = new CshoppingCartViewModel()
            {
                TravelProductId = s.TravelProductId,
                TravelProductName = s.TravelProductName,
                Count = s.Count,
                Price = s.Price,
                Productpicture = s.Productpicture,
            };
            list.Add(item);
            jsonCart = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCT, jsonCart);


            //string jsonBurchased = JsonSerializer.Serialize(s);
            //HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCT, jsonBurchased);
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
        public IActionResult ShoppingCartSession()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                List<CshoppingCartViewModel> cart = JsonSerializer.Deserialize<List<CshoppingCartViewModel>>(jsonCart);
                return View(cart);
            }
            else
                return View();
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
        public IActionResult testPage()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                List<CshoppingCartViewModel> cart = JsonSerializer.Deserialize<List<CshoppingCartViewModel>>(jsonCart);
                return View(cart);
            }
            else
                return View();
        }
        [HttpPost]
        public IActionResult PayTest(CshoppingCartViewModel t)
        {
            var data = t;
            return View(data);
        }
    }
}
