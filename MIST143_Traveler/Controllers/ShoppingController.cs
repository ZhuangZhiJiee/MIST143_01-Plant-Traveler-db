﻿using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using MIST143_Traveler.ShoppingViewModel;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace MIST143_Traveler.Controllers
{
    public class ShoppingController : Controller
    {
        private PlanetTravelContext pt;
        public ShoppingController(PlanetTravelContext q) 
        {
            pt = q;
        }
           
        public IActionResult List(int? TravelProductId,int? MembersId)
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
                    Productpictures = s.TravelPictures.ToList(),
                    Date = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.Date).ToList(),
                    HotelName = s.TravelProductDetails.Where(p => p.TravelProductId == s.TravelProductId).Select(p => p.Hotel.HotelName).ToList(),
                    DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                      _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                    {
                        ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                    }).ToList(),
                }).FirstOrDefault();
           
            if (MembersId != null)
            {
                var d = pt.Members.FirstOrDefault(a => a.MembersId == MembersId);
                var myid = pt.Myfavorites.FirstOrDefault(a => a.MembersId == MembersId&&a.TravelProductId== TravelProductId);
                if (myid != null)
                {
                    CProductViewModel myfaid = pt.TravelProducts.Where(p => p.TravelProductId == TravelProductId)
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
                   Productpictures = s.TravelPictures.ToList(),
                   Date = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.Date).ToList(),
                   HotelName = s.TravelProductDetails.Where(p => p.TravelProductId == s.TravelProductId).Select(p => p.Hotel.HotelName).ToList(),
                   DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                   MembersId = d.MembersId,
                   MyfavoritesID= s.Myfavorites.Where(w => w.MembersId == MembersId && s.TravelProductId == myid.TravelProductId).Any(),
                    _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                   {
                       ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                   }).ToList(),
               }).FirstOrDefault();
                 
                    return View(myfaid);
                }

                CProductViewModel prMid = pt.TravelProducts.Where(p => p.TravelProductId == TravelProductId)
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
                   Productpictures = s.TravelPictures.ToList(),
                   Date = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.Date).ToList(),
                   HotelName = s.TravelProductDetails.Where(p => p.TravelProductId == s.TravelProductId).Select(p => p.Hotel.HotelName).ToList(),
                   DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                   MembersId = d.MembersId,
                  
                   _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                   {
                       ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                   }).ToList(),
               }).FirstOrDefault();
                return View(prMid);
            }
            return View(prod);
            
        }
        [HttpPost]
        public IActionResult List(CtoMyFavorites iid)
        {

            var cc = pt.Myfavorites.FirstOrDefault(s => s.MembersId == iid.myfavorite.MembersId && s.TravelProductId == iid.TravelProductId);
            Myfavorite ss = iid.myfavorite;
            pt.Myfavorites.Remove(cc);
            pt.SaveChanges();
            return Json(new {Res=true});
        }
        public IActionResult toMyFavorites(CtoMyFavorites PMID)
        {
            if (PMID != null)
            {
                var Cus = pt.Members.FirstOrDefault(a => a.MembersId == PMID.MembersId);
                if (Cus != null)
                {

                    var cc = pt.Myfavorites.Where(s => s.MembersId == PMID.MembersId && s.TravelProductId == PMID.TravelProductId).ToList();

                    if (cc.Count > 0)
                    {
                        return Json(new { Res = false });
                    }
                    Myfavorite fa = PMID.myfavorite;
                    pt.Add(fa);
                    pt.SaveChanges();
                    return Json(new { Res = true });
                }
                return Json(new { Res = false });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddToSession(CAddToSessionViewModel s) 
        {
            string jsonCart = "";
            List<CShoppingCartDetailViewModel> list = null;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
                list = new List<CShoppingCartDetailViewModel>();
            else
            {
                jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                list = JsonSerializer.Deserialize<List<CShoppingCartDetailViewModel>>(jsonCart);
            }
            CShoppingCartDetailViewModel item = new CShoppingCartDetailViewModel()
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

            return NoContent();
        }

        public IActionResult ShoppingCartSession()
        {
            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
            string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
            CShoppingCartViewModel scv = new CShoppingCartViewModel()
            {
                MemberName = v.MemberName,
                Email = v.Email,
                Phone = v.Phone,
                _CShoppingCartDetailViewModel = JsonSerializer.Deserialize<List<CShoppingCartDetailViewModel>>(jsonCart),
            };

            return View(scv);
        }
        [HttpPost]
        public IActionResult PayData(CShoppingCartViewModel p)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT) == null)
            {
                var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
                var v = JsonSerializer.Deserialize<Member>(Name);
                CShoppingCartViewModel scv = new CShoppingCartViewModel()
                {
                    MemberName = v.MemberName,
                    Email = v.Email,
                    Phone = v.Phone,
                    _CShoppingCartDetailViewModel = p._CShoppingCartDetailViewModel,
                };
                return View(scv);
            }
            else 
            return View(p);

        }
        [HttpPost]
        public IActionResult PayCheckout(CShoppingCartViewModel p)
        {
            
            return View(p);
        }
        [HttpPost]
        public IActionResult PayEnd(CShoppingCartViewModel p)
        {
            //歐付寶範圍
            if (p.PaymethodId == 4)
            {
                //string ProductName = "";
                #region 金流支付
                string tradeNo = Guid.NewGuid().ToString();
                tradeNo = tradeNo.Substring(tradeNo.Length - 12, 12);
                ViewBag.tradeNo = tradeNo;
                string timenow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                ViewBag.timenow = timenow;

                int total = 0;
                string ItemName = "";
                //只買一組的寫法，多組要改掉
                //int price = (int)p.UnitPrice;
                //total += ((int)(p.Quantity) * price);
                //ItemName += $"{ProductName} NT$ {Convert.ToInt32(p.UnitPrice).ToString("0")}元 x {p.Quantity}組#";

                //ViewModel的屬性改成List後再用迴圈一個個取出
                foreach (var item in p._CPayViewModel)
                {
                    //ProductName += pt.TravelProducts.FirstOrDefault(c => c.TravelProductId == item.TravelProductId).TravelProductName;
                    int price = (int)item.Price;
                    total += ((int)(item.Count) * price);
                    ItemName += $"{item.TravelProductName} NT${Convert.ToInt32(item.Price).ToString("0")}元 x {item.Count}組#";
                }

                //有優惠再去扣除優惠額
                //total = total - discountmoney;
                //if (total < 1200) { total = total + 100; }

                ItemName = ItemName.Substring(0, ItemName.Length - 1);

                ViewBag.Total = total;
                ViewBag.ItemName = ItemName;

                NameValueCollection parameters = HttpUtility.ParseQueryString(string.Empty);

                parameters["HashKey"] = "5294y06JbISpM5x9";
                parameters["ChoosePayment"] = "Credit";
                parameters["ClientBackURL"] = $"{Request.Scheme}://{Request.Host}/Home/Index";    //完成後跳回去的頁面
                parameters["CreditInstallment"] = "";
                parameters["EncryptType"] = "1";
                parameters["InstallmentAmount"] = "";
                parameters["ItemName"] = ItemName;
                parameters["MerchantID"] = "2000132";
                parameters["MerchantTradeDate"] = timenow;
                parameters["MerchantTradeNo"] = tradeNo;
                parameters["PaymentType"] = "aio";
                parameters["Redeem"] = "";
                parameters["ReturnURL"] = "https://developers.opay.tw/AioMock/MerchantReturnUrl";
                parameters["StoreID"] = "";
                parameters["TotalAmount"] = total.ToString();
                parameters["TradeDesc"] = "建立信用卡測試訂單";
                parameters["HashIV"] = "v77hoKGq4kWxNNIS";

                ViewBag.ClientBackURL = $"{Request.Scheme}://{Request.Host}/Home/Index";

                string checkMacValue = parameters.ToString();

                checkMacValue = checkMacValue.Replace("=", "%3d").Replace("&", "%26");

                using var hash = SHA256.Create();
                var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(checkMacValue.ToLower()));
                checkMacValue = Convert.ToHexString(byteArray).ToUpper();
                ViewBag.checkMacValue = checkMacValue;
                #endregion
                return View();
            }
            //========================結帳頁面=========================================================
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
            for (int i = 0; i < p._CPayViewModel.Count; i++) 
            {
                OrderDetail odd = new OrderDetail()
                {
                    OrderId = pt.Orders.OrderBy(e => e.OrderId).LastOrDefault().OrderId,
                    TravelProductId = p._CPayViewModel[i].TravelProductId,
                    Quantity = p._CPayViewModel[i].Count,
                    UnitPrice = p._CPayViewModel[i].Price,
                };
                pt.OrderDetails.Add(odd);
            }
            pt.SaveChanges();
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Index", "Home");
        }
    }
}
