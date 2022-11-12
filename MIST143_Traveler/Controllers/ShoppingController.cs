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
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MIST143_Traveler.Controllers
{
    public class ShoppingController : Controller
    {
        private PlanetTravelContext pt;
        public ShoppingController(PlanetTravelContext q)
        {
            pt = q;
        }

        public IActionResult List(int? TravelProductId, int? MembersId)
        {
            //---------------------------------------------------------
            List<Cproductlist> tp = new List<Cproductlist>();
            var pro = HttpContext.Session.GetString(CDictionary.SK_PRODUCT);
            var plk = pt.TravelProducts.FirstOrDefault(o => o.TravelProductId == TravelProductId);
            var q = pt.TravelPictures.Where(o => o.TravelProductId == TravelProductId).Select(i => new Cproductlist
            {
                travelProduct = i.TravelProduct,
                productpicture = i.TravelPicture1,
            }).FirstOrDefault();
            //var q1 = pt.TravelPictures.Where(i=>i.TravelProduct.TravelProductType)
            if (pro != null)
            {
                var poi = JsonSerializer.Deserialize<List<Cproductlist>>(pro);
                bool ch = true;
                foreach (var i in poi)
                {
                    if (i.TravelProductId == TravelProductId)
                    {
                        ch = false;

                    }
                }
                if (ch)
                {
                    poi.Add(q);
                    var tye = JsonSerializer.Serialize(poi);
                    HttpContext.Session.SetString(CDictionary.SK_PRODUCT, tye);
                }
            }
            else
            {
                tp.Add(q);
                var dfy = JsonSerializer.Serialize(tp);
                HttpContext.Session.SetString(CDictionary.SK_PRODUCT, dfy);
            }


            //-----------------------------------------------

            //======================計算剩餘天數========================================================
            DateTime deparaturedate = Convert.ToDateTime(pt.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).FirstOrDefault().Date);
            DateTime datetoday = DateTime.Today;
            double remainingdays = new TimeSpan(deparaturedate.Ticks - datetoday.Ticks).TotalDays;

            //=========================資料存ViewModel==================================================
            CProductViewModel prod = pt.TravelProducts.Where(p => p.TravelProductId == TravelProductId)
                .Select(s => new CProductViewModel
                {
                    //ClickId =
                    TravelProductName = s.TravelProductName,
                    TravelProductId = s.TravelProductId,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    Stocks = s.Stocks,
                    DeparatureDate = s.TravelProductDetails.FirstOrDefault().Date,
                    AnotherDepartureDate = s.AnotherDepartureDate,
                    RemainingDays = remainingdays,
                    Description = s.Description,
                    EventIntroduction = s.EventIntroduction,
                    MapUrl = s.MapUrl,
                    CountryName = s.Country.CountryName,
                    PreparationDescription = s.PreparationDescription,
                    Productpictures = s.TravelPictures.ToList(),
                    DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                    _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                    {
                        Date = p.Date,
                        HotelName = p.Hotel.HotelName,
                        ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                    }).ToList(),
                    _CCommentViewModel = pt.Comments.Where(a => a.TravelProductId == TravelProductId && a.CommentStatus == true).Select(b => new CCommentViewModel
                    {
                        MemberName = b.Members.MemberName,
                        PhotoPath = b.Members.PhotoPath,
                        CommentText = b.CommentText,
                        Star = b.Star,
                        CommentDate = b.CommentDate,
                    }).ToList(),
                }).FirstOrDefault();

            if (MembersId != null)
            {
                var d = pt.Members.FirstOrDefault(a => a.MembersId == MembersId);
                var myid = pt.Myfavorites.FirstOrDefault(a => a.MembersId == MembersId && a.TravelProductId == TravelProductId);
                if (myid != null)
                {
                    CProductViewModel myfaid = pt.TravelProducts.Where(p => p.TravelProductId == TravelProductId)
               .Select(s => new CProductViewModel
               {

                   TravelProductName = s.TravelProductName,
                   TravelProductId = s.TravelProductId,
                   Price = s.Price,
                   Quantity = s.Quantity,
                   Stocks = s.Stocks,
                   DeparatureDate = s.TravelProductDetails.FirstOrDefault().Date,
                   AnotherDepartureDate = s.AnotherDepartureDate,
                   RemainingDays = remainingdays,
                   Description = s.Description,
                   EventIntroduction = s.EventIntroduction,
                   MapUrl = s.MapUrl,
                   PreparationDescription = s.PreparationDescription,
                   Productpictures = s.TravelPictures.ToList(),
                   DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                   MembersId = d.MembersId,
                   MyfavoritesID = s.Myfavorites.Where(w => w.MembersId == MembersId && s.TravelProductId == myid.TravelProductId).Any(),
                   _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                   {
                       Date = p.Date,
                       HotelName = p.Hotel.HotelName,
                       ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                   }).ToList(),
                   _CCommentViewModel = pt.Comments.Where(a => a.TravelProductId == TravelProductId && a.CommentStatus == true).Select(b => new CCommentViewModel
                   {
                       MemberName = b.Members.MemberName,
                       PhotoPath = b.Members.PhotoPath,
                       CommentText = b.CommentText,
                       Star = b.Star,
                       CommentDate = b.CommentDate,
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
                   Quantity = s.Quantity,
                   Stocks = s.Stocks,
                   DeparatureDate = s.TravelProductDetails.FirstOrDefault().Date,
                   AnotherDepartureDate = s.AnotherDepartureDate,
                   RemainingDays = remainingdays,
                   Description = s.Description,
                   EventIntroduction = s.EventIntroduction,
                   MapUrl = s.MapUrl,
                   PreparationDescription = s.PreparationDescription,
                   Productpictures = s.TravelPictures.ToList(),
                   DailyDetailText = s.TravelProductDetails.Where(a => a.TravelProductId == TravelProductId).Select(a => a.DailyDetailText).ToList(),
                   MembersId = d.MembersId,



                   _CProductDetailViewModel = s.TravelProductDetails.Select(p => new CProductDetailViewModel
                   {
                       Date = p.Date,
                       HotelName = p.Hotel.HotelName,
                       ViewName = p.ProductToViews.Select(v => v.View.ViewName).ToList(),
                   }).ToList(),
                   _CCommentViewModel = pt.Comments.Where(a => a.TravelProductId == TravelProductId && a.CommentStatus == true).Select(b => new CCommentViewModel
                   {
                       MemberName = b.Members.MemberName,
                       PhotoPath = b.Members.PhotoPath,
                       CommentText = b.CommentText,
                       Star = b.Star,
                       CommentDate = b.CommentDate,
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
            return Json(new { Res = true });
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
        public IActionResult RemoveSession(int? TravelProductId)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
            {
                List<CShoppingCartDetailViewModel> list = null;
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                list = JsonSerializer.Deserialize<List<CShoppingCartDetailViewModel>>(jsonCart);

                int index = list.FindIndex(m => m.TravelProductId == TravelProductId);
                list.RemoveAt(index);
                jsonCart = JsonSerializer.Serialize(list);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCT, jsonCart);
            }

            return RedirectToAction("ShoppingCartSession");
        }
        public IActionResult ShoppingCartSession()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_Login) != null)
            {
                var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
                var v = JsonSerializer.Deserialize<Member>(Name);
                if (HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT) != null)
                {
                    string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                    CShoppingCartViewModel scv = new CShoppingCartViewModel()
                    {
                        MembersId = v.MembersId,
                        MemberName = v.MemberName,
                        Email = v.Email,
                        Phone = v.Phone,
                        _CShoppingCartDetailViewModel = JsonSerializer.Deserialize<List<CShoppingCartDetailViewModel>>(jsonCart),
                    };
                    return View(scv);
                }
                else
                    return NoContent();
            }
            else
                return RedirectToAction("newLoginpag", "CustomerCenter");

        }
        [HttpPost]
        public IActionResult PayData(CShoppingCartViewModel p)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_Login) != null)
            {
                var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
                var v = JsonSerializer.Deserialize<Member>(Name);
                CShoppingCartViewModel scv = new CShoppingCartViewModel()
                {
                    MembersId = v.MembersId,
                    MemberName = v.MemberName,
                    Email = v.Email,
                    Phone = v.Phone,
                    _CShoppingCartDetailViewModel = p._CShoppingCartDetailViewModel,
                };
                return View(scv);
            }
            else
                return RedirectToAction("newLoginpag", "CustomerCenter");
        }
        [HttpPost]
        public IActionResult PayCheckout(CShoppingCartViewModel p)
        {
            CShoppingCartViewModel scv = new CShoppingCartViewModel()
            {
                MembersId = p.MembersId,
                MemberName = p.MemberName,
                Email = p.Email,
                Phone = p.Phone,
                _CShoppingCartDetailViewModel = p._CShoppingCartDetailViewModel,
                _CCouponViewModel = pt.CouponLists.Where(a => a.MembersId == p.MembersId && a.CouponStatus == true).Select(b => new CCouponViewModel
                {
                    CouponId = b.Coupon.CouponId,
                    CouponListId = b.CouponListId,
                    CouponName = b.Coupon.CouponName,
                    Discount = b.Coupon.Discount,
                    Condition = b.Coupon.Condition,
                    ExDate = b.Coupon.ExDate,
                }).ToList(),
            };

            return View(scv);
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
                //===========================歐附寶存入資料庫===================================================
                #region
                
                if (p.CouponId == 0)
                {
                    Order odn = new Order()
                    {
                        MembersId = p.MembersId,
                        PaymentId = p.PaymethodId,
                        OrderDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        OrderStatusId = 3,
                    };
                    pt.Orders.Add(odn);
                }
                else
                {
                    Order od = new Order()
                    {
                        MembersId = p.MembersId,
                        PaymentId = p.PaymethodId,
                        OrderDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        CouponId = p.CouponId,
                        OrderStatusId = 3,
                    };
                    pt.Orders.Add(od);
                    CouponList couponList = pt.CouponLists.FirstOrDefault(t => t.CouponListId == p.CouponListId);
                    if (couponList != null)
                    {
                        couponList.CouponStatus = false;
                    }
                }

                pt.SaveChanges();

                for (int i = 0; i < p._CPayViewModel.Count; i++)
                {
                    if (p._CPayViewModel[i].AccompanyPeople != null)
                    {
                        OrderDetail odd = new OrderDetail()
                        {
                            OrderId = pt.Orders.OrderBy(e => e.OrderId).LastOrDefault().OrderId,
                            TravelProductId = p._CPayViewModel[i].TravelProductId,
                            Quantity = p._CPayViewModel[i].Count,
                            UnitPrice = p._CPayViewModel[i].Price,
                            AccompanyPeople = p._CPayViewModel[i].AccompanyPeople,
                        };
                        pt.OrderDetails.Add(odd);
                    }
                    else
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
                }

                for (int i = 0; i < p._CPayViewModel.Count; i++)
                {
                    TravelProduct prod = pt.TravelProducts.FirstOrDefault(t => t.TravelProductId == p._CPayViewModel[i].TravelProductId);
                    if (prod != null)
                    {
                        prod.Stocks = prod.Stocks - p._CPayViewModel[i].Count;
                    }
                }
                pt.SaveChanges();
                //================================清除Session=======================================================================
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
                {
                    List<CShoppingCartDetailViewModel> list = null;
                    string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                    list = JsonSerializer.Deserialize<List<CShoppingCartDetailViewModel>>(jsonCart);

                    list.Clear();
                    jsonCart = JsonSerializer.Serialize(list);
                    HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCT, jsonCart);
                }
                return View();
            }
            #endregion

            //========================結帳頁面-存入資料庫=========================================================
            else
            {
                if (p.CouponId == 0)
                {
                    Order odn = new Order()
                    {
                        MembersId = p.MembersId,
                        PaymentId = p.PaymethodId,
                        OrderDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        OrderStatusId = 3,
                    };
                    pt.Orders.Add(odn);
                }
                else
                {
                    Order od = new Order()
                    {
                        MembersId = p.MembersId,
                        PaymentId = p.PaymethodId,
                        OrderDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        CouponId = p.CouponId,
                        OrderStatusId = 3,
                    };
                    pt.Orders.Add(od);
                    CouponList couponList = pt.CouponLists.FirstOrDefault(t => t.CouponListId == p.CouponListId);
                    if (couponList != null)
                    {
                        couponList.CouponStatus = false;
                    }
                }

                pt.SaveChanges();

                for (int i = 0; i < p._CPayViewModel.Count; i++)
                {
                    if (p._CPayViewModel[i].AccompanyPeople != null)
                    {
                        OrderDetail odd = new OrderDetail()
                        {
                            OrderId = pt.Orders.OrderBy(e => e.OrderId).LastOrDefault().OrderId,
                            TravelProductId = p._CPayViewModel[i].TravelProductId,
                            Quantity = p._CPayViewModel[i].Count,
                            UnitPrice = p._CPayViewModel[i].Price,
                            AccompanyPeople = p._CPayViewModel[i].AccompanyPeople,
                        };
                        pt.OrderDetails.Add(odd);
                    }
                    else
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
                }

                for (int i = 0; i < p._CPayViewModel.Count; i++)
                {
                    TravelProduct prod = pt.TravelProducts.FirstOrDefault(t => t.TravelProductId == p._CPayViewModel[i].TravelProductId);
                    if (prod != null)
                    {
                        prod.Stocks = prod.Stocks - p._CPayViewModel[i].Count;
                    }
                }
                pt.SaveChanges();
                //================================清除Session=======================================================================
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT))
                {
                    List<CShoppingCartDetailViewModel> list = null;
                    string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT);
                    list = JsonSerializer.Deserialize<List<CShoppingCartDetailViewModel>>(jsonCart);

                    list.Clear();
                    jsonCart = JsonSerializer.Serialize(list);
                    HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCT, jsonCart);
                }
                //=================================寄信=============================================================================
                MimeMessage message = new MimeMessage();
                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = $"<p>您好，恭喜您付款已完成，祝您旅途愉快!</p>" +
                                   $"<p>提醒您現在為疫情期間，請多留意自身健康安全</p>" +
                                  $"<div style='border: 1px solid black;text-align: center;'>      </div>" +
                                  $"<p>PlanetTraveler星球旅遊 關心您</p>" +
                                  $"<p>傳送時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>";



                message.From.Add(new MailboxAddress("PlanetTraveler星球旅遊", "planettravelermsit143@outlook.com"));
                message.To.Add(new MailboxAddress("親愛的顧客", p.Email));
                message.Subject = "恭喜付款完成";
                message.Body = builder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.outlook.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("planettravelermsit143@outlook.com", "gogo1116");
                    client.Send(message);
                    client.Disconnect(true);
                }

                System.Threading.Thread.Sleep(3000);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
