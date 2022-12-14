using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.Models.miViewModel;
using MimeKit;
using MailKit.Net.Smtp;
using MIST143_Traveler.MViewModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using System.Security.Cryptography;
using System.Text;
using MIST143_Traveler.Authorization;
using Microsoft.AspNetCore.Authorization;
//using System.Net.Mail;
using System.Net.Mime;
using MimeKit.Utils;
using System.Runtime.Versioning;

namespace MIST143_Traveler.Controllers
{
    //[Authorize]
    public class CustomerCenterController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly PlanetTravelContext _PlanetTravelContext;
        private IWebHostEnvironment _enviro;
        public static string randomCode;
        public CustomerCenterController(PlanetTravelContext PlanetTrave, IWebHostEnvironment p, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _PlanetTravelContext = PlanetTrave;
            _enviro = p;
        }

        public IActionResult Index()
        {
            return PartialView();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(CDictionary.SK_Login);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult newLoginpag()
        {
            return View();
        }
        //========================訂單明細顯示=======================
        public IActionResult OrderDE(int id)
        {

            OrderDetail od = _PlanetTravelContext.OrderDetails.FirstOrDefault(a => a.OrderId == id);
            COrderDetail co = new COrderDetail();
            co.訂單 = (from q in _PlanetTravelContext.Orders.Where(w => w.OrderId == id)
                     from a in _PlanetTravelContext.OrderDetails.Where(a => a.OrderId == id)
                     from b in _PlanetTravelContext.TravelProducts.Where(s => s.TravelProductId == a.TravelProductId)

                     select new 訂單管理
                     {

                         隨行人員 = a.AccompanyPeople,
                         TravelProductId = b.TravelProductId,
                         訂單編號 = a.OrderId,
                         商品名稱 = b.TravelProductName,
                         數量 = a.Quantity,
                         購買金額 = (int)a.UnitPrice,
                         訂購日期 = a.Order.OrderDate,
                         優惠券 = q.Coupon.CouponName,
                         付款方式 = q.Payment.PaymentName,
                         折扣金額 = q.Coupon.Discount,

                     }).ToList();
            foreach (var item in co.訂單)
            {
                item.FImagePath = _PlanetTravelContext.TravelPictures.Where(a => a.TravelProductId == item.TravelProductId).Select(s => s.TravelPicture1).ToList();
            }


            return View(co);
        }

        //====================================AJAX上傳圖片=========================
        public IActionResult FileUpLoad(IFormFile files)
        {
            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == v.MembersId);

            if (files != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Cus.PhotoPath = pName;
                string path = _enviro.WebRootPath + "/Images/客戶大頭貼/" + pName;
                using (var steam = new FileStream(path, FileMode.Create))
                {
                    files.CopyTo(steam);
                    _PlanetTravelContext.SaveChanges();

                }
                string p = "/Images/客戶大頭貼/" + pName;
                string dd = pName;
                v.PhotoPath = dd;
                var s = JsonSerializer.Serialize(v);
                HttpContext.Session.SetString(CDictionary.SK_Login, s);


                return Json(p);
                //return new ObjectResult(new { status = "success" });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }

        [HttpPost]
        public IActionResult SaveCusInfo(CMemberView inCus)
        {
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCus.MembersId);

            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
            if (Cus != null)
            {
                if (inCus.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";//創造一個唯一路徑
                    Cus.PhotoPath = pName;
                    string path = _enviro.WebRootPath + "/Images/客戶大頭貼/" + pName;//此為網頁路徑  
                    inCus.photo.CopyTo(new FileStream(path, FileMode.Create));//FileStream這串流需要兩個參數1.網路路徑OR專案路徑2.FILEMODE
                }
                Cus.Address = inCus.Address;
                Cus.MemberName = inCus.MemberName;

                //測試
                string Cname = inCus.MemberName;
                v.MemberName = Cname;
                var s = JsonSerializer.Serialize(v);
                HttpContext.Session.SetString(CDictionary.SK_Login, s);


                Cus.Phone = inCus.Phone;
                Cus.CityId = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == inCus.Cityname).CityId;

                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, data = Cname });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }
        //=============================變更密碼頁============================
        public IActionResult PasswordChange(CPasswordChange inCP)
        {
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCP.MembersId);
            if (Cus != null)
            {
                if (inCP.舊密碼 == Cus.Password && inCP.新密碼 == inCP.確認新密碼)
                {
                    Cus.Password = inCP.確認新密碼;
                    _PlanetTravelContext.SaveChanges();
                    return Json(new { Res = true, Msg = "成功" });
                }
                return Json(new { Res = false, Msg = "失敗" });
            }
            return ViewComponent("PasswordChange");
        }
        //=============================變更密碼頁============================


        public IActionResult List(int MembersId)
        {
            CMemberView cm = new CMemberView();
            if (MembersId != 0)
            {
                _PlanetTravelContext.Coupons.ToList();//如果在同一個變數想WHERE兩張表的內容需要先用TOLIST先把他接住
                var cl = _PlanetTravelContext.CouponLists.ToList();
                var 時間 = DateTime.Now.AddDays(-1).ToShortDateString();

                //var c = _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId && a.CouponStatus == true).Count();
                var c = cl.Where(a => a.MembersId == MembersId && a.CouponStatus == true &&
                Convert.ToDateTime(a.Coupon.ExDate) > Convert.ToDateTime(時間)).Count();

                var myf = _PlanetTravelContext.Myfavorites.Where(a => a.MembersId == MembersId).Count();


                cm = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(a => new CMemberView
                {
                    myccount = c,
                    myfcount = myf,
                    member = a,
                    Cityname = a.City.CityName,

                }).FirstOrDefault();

                if (cm != null)
                {
                    return View(cm);
                }
            }

            return View("test");
        }
        public IActionResult TextM(int? MembersId)
        {
            CMemberView Cus = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(inCus => new CMemberView
            {
                Address = inCus.Address,
                //Password = inCus.Password,
                MemberName = inCus.MemberName,
                Phone = inCus.Phone,
                FImagePath = inCus.PhotoPath,

            }).FirstOrDefault();


            return View();

        }

        public IActionResult GetCaptcha()
        {
            randomCode = CCaptcha.CreateRandomCode(5).ToLower();
            byte[] captcha = CCaptcha.CreatImage(randomCode);
            return File(captcha, "image/jpeg");
        }

        //左邊功能開始
        [AllowAnonymous]
        [HttpPost]
        public IActionResult newLoginpag(CLogin vModel)
        {

            string randomCode = CustomerCenterController.randomCode;
            Member cust = _PlanetTravelContext.Members.FirstOrDefault
              (c => c.Email.Equals(vModel.Email));
            if (cust != null)
            {
                if (vModel.CAPTCHA != null)
                {
                    if (vModel.CAPTCHA.ToLower() == randomCode)
                    {

                        if (cust.Password.Equals(vModel.Password))
                        {

                            string jsonUser = JsonSerializer.Serialize(cust);
                            HttpContext.Session.SetString(
                                CDictionary.SK_Login, jsonUser);
                            //if (vModel.KeepLogin == "true")
                            //{
                            //    CookieOptions option = new CookieOptions();
                            //    option.Expires = DateTime.Now.AddMinutes(60);
                            //    int index = vModel.Email.IndexOf("@");
                            //    string email = vModel.Email.Substring(0,index);
                            //    Response.Cookies.Append(email, vModel.Password, option);
                            //}

                            return Json(new { Res = true, Msg = "成功" });
                        }
                        else
                            return Json(new { Res = false, Msg = "失敗" });
                    }
                }
                
                else
                return Json(new { Res = false, Msg = "請確認驗證碼" });
            }
            else
                return Json(new { Res = false, Msg = "失敗" });
            return View();
        }



        public IActionResult ProductManage()
        {
            return View();
        }
        public IActionResult newProductManage()
        {
            return View();
        }

        public IActionResult Myfavorites()
        {
            return PartialView();
        }
        public IActionResult Review()
        {
            return PartialView();
        }
        //============================檢視優惠券頁==============================
        public IActionResult VIEWCoupon(int MembersId)
        {
            會員中心檢視優惠券 Coup = new 會員中心檢視優惠券();
            if (MembersId != 0)
            {

                Coup.優惠券列表 = (from Cuu in _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId)
                              from Cup in _PlanetTravelContext.Coupons.Where(s => s.CouponId == Cuu.CouponId)
                              select new 我的優惠券
                              {
                                  MembersId = MembersId,
                                  CouponId = Cup.CouponId,
                                  CouponName = Cup.CouponName,
                                  Condition = Cup.Condition,
                                  Discount = Cup.Discount,
                                  ExDate = Cup.ExDate,
                                  CouponStatus = Cuu.CouponStatus,
                              }
                            ).ToList();

            }
            var ex = Coup.優惠券列表.Where(a => DateTime.Parse(a.ExDate) >= DateTime.Now).ToList();
            return ViewComponent("Coupon", ex);
        }

        //==========================Coupon======================
        public IActionResult CouponAll(int MembersId)
        {
            會員中心檢視優惠券 Coup = new 會員中心檢視優惠券();
            if (MembersId != 0)
            {

                Coup.優惠券列表 = (from Cuu in _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId)
                              from Cup in _PlanetTravelContext.Coupons.Where(s => s.CouponId == Cuu.CouponId)

                              select new 我的優惠券
                              {
                                  MembersId = MembersId,
                                  CouponId = Cup.CouponId,
                                  CouponName = Cup.CouponName,
                                  Condition = Cup.Condition,
                                  Discount = Cup.Discount,
                                  ExDate = Cup.ExDate,
                                  CouponStatus = Cuu.CouponStatus,
                              }
                            ).ToList();
                var ex = Coup.優惠券列表.Where(a => DateTime.Parse(a.ExDate) > DateTime.Now).ToList();
                if (ex != null)
                {
                    return ViewComponent("CouponAll", ex);
                }
            }
            return ViewComponent("CouponAll", Coup);
        }
        public IActionResult CouponExp(int MembersId)
        {
            會員中心檢視優惠券 Coup = new 會員中心檢視優惠券();
            if (MembersId != 0)
            {

                Coup.優惠券列表 = (from Cuu in _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId)
                              from Cup in _PlanetTravelContext.Coupons.Where(s => s.CouponId == Cuu.CouponId)

                              select new 我的優惠券
                              {
                                  MembersId = MembersId,
                                  CouponId = Cup.CouponId,
                                  CouponName = Cup.CouponName,
                                  Condition = Cup.Condition,
                                  Discount = Cup.Discount,
                                  ExDate = Cup.ExDate,
                                  CouponStatus = Cuu.CouponStatus,
                              }
                            ).ToList();
                var ex = Coup.優惠券列表.Where(a => DateTime.Parse(a.ExDate) <= DateTime.Now).ToList();
                if (ex != null)
                {

                    return ViewComponent("CouponExp", ex);
                }
            }
            return ViewComponent("CouponAll", Coup);
        }

        public IActionResult CouponClose(int MembersId)
        {
            會員中心檢視優惠券 Coup = new 會員中心檢視優惠券();
            if (MembersId != 0)
            {

                Coup.優惠券列表 = (from Cuu in _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId)
                              from Cup in _PlanetTravelContext.Coupons.Where(s => s.CouponId == Cuu.CouponId)

                              select new 我的優惠券
                              {
                                  MembersId = MembersId,
                                  CouponId = Cup.CouponId,
                                  CouponName = Cup.CouponName,
                                  Condition = Cup.Condition,
                                  Discount = Cup.Discount,
                                  ExDate = Cup.ExDate,
                                  CouponStatus = Cuu.CouponStatus,
                              }
                            ).ToList();
                var tit = DateTime.Now.AddDays(7).ToShortDateString();
                var 昨天 = DateTime.Now.AddDays(-1).ToShortDateString();

                var exe = Coup.優惠券列表.Where(a => DateTime.Parse(a.ExDate) < DateTime.Parse(tit)).ToList();
                var ex = exe.Where(b => DateTime.Parse(b.ExDate) > DateTime.Parse(昨天)).ToList();
                if (ex != null)
                {
                    return ViewComponent("CouponClose", ex);
                }
            }
            return ViewComponent("CouponAll", Coup);
        }
        //==========================Coupon======================
        public IActionResult Giftkey(會員中心檢視優惠券 Cou)
        {

            var q = _PlanetTravelContext.Coupons.FirstOrDefault(a => a.GiftKey == Cou.GiftKey);
            if (q != null)
            {
                var use = _PlanetTravelContext.CouponLists.FirstOrDefault(s => s.MembersId == Cou.MembersId && s.CouponId == q.CouponId);

                if (use == null)
                {
                    CouponList li = new CouponList
                    {
                        CouponId = q.CouponId,
                        CouponStatus = true,
                        MembersId = Cou.MembersId,
                    };
                    會員中心檢視優惠券 動態 = new 會員中心檢視優惠券
                    {
                        CouponId = q.CouponId,
                        Condition = q.Condition,
                        ExDate = q.ExDate,
                        Useful = (bool)q.Useful,
                        Discount = q.Discount,
                        CouponName = q.CouponName
                    };
                    _PlanetTravelContext.CouponLists.Add(li);
                    _PlanetTravelContext.SaveChanges();
                    return Json(new { Res = true, Msg = "成功", data = 動態 });
                }
                return Json(new { Res = false, Msg = "已經使用過" });
            }
            return Json(new { Res = false, Msg = "無效的優惠" });
        }
        //============================檢視優惠券頁==============================
        public IActionResult Coupon()
        {
            return PartialView();
        }
        public IActionResult Star()
        {
            return PartialView();
        }

        public IActionResult CustomerInfo(int MembersId)
        {

            CMemberView cm = new CMemberView();
            if (MembersId != 0)
            {
                cm = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(a => new CMemberView
                {
                    member = a,
                    Cityname = a.City.CityName,

                }).FirstOrDefault();

                if (cm != null)
                {
                    return View(cm);
                }
            }

            return View("test");
        }
        [HttpPost]
        public IActionResult CustomerInfo(CMemberView inCus)
        {

            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCus.MembersId);

            if (Cus != null)
            {
                Cus.Address = inCus.Address;
                Cus.Password = inCus.Password;
                Cus.MemberName = inCus.MemberName;
                Cus.Phone = inCus.Phone;
                Cus.CityId = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == inCus.Cityname).CityId;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }
        //左邊功能結束
        public IActionResult test()
        {

            return View();
        }


        public IActionResult Order(int MembersId)
        {
            if (MembersId != 0)
            {
                var q = _PlanetTravelContext.Orders.Where(a => a.MembersId == MembersId);
                List<Corderview> Cord = new List<Corderview>();

                Cord = (from od in _PlanetTravelContext.Orders.Where(a => a.MembersId == MembersId)
                            //from p in od.OrderDetails.Where(a => a.OrderId == od.OrderId)
                        select new Corderview
                        {
                            orderId = od.OrderId,
                            訂單編號 = od.OrderId,
                            商品名稱 = od.OrderDetails.Select(a => a.TravelProduct.TravelProductName).FirstOrDefault(),
                            訂單狀態 = od.OrderStatus.OrderStatusName,
                            訂購日期 = DateTime.Parse(od.OrderDate).ToString("yyyy-MM-dd"),
                            購買金額 = od.OrderDetails.Select(a => a.UnitPrice).FirstOrDefault(),

                            orderItem = _PlanetTravelContext.OrderDetails.Where(i => i.OrderId == od.OrderId).
                                 Select(c => new CorderItemView
                                 {
                                     訂單編號 = c.OrderId,//偉庭交嘉義教的
                                     購買金額 = c.UnitPrice,
                                     數量 = c.Quantity,
                                 }
                                 ).ToList(),

                            折扣 = od.Coupon.Discount,
                            評論狀態 = od.Members.Comments.Where(a => a.MembersId == od.MembersId && a.OrderId == od.OrderId).Count(),
                            數量 = od.OrderDetails.Select(a => a.Quantity).FirstOrDefault(),

                        }).ToList();
                var dsd = JsonSerializer.Serialize(Cord);
                if (Cord.Count > 0)
                {
                    return ViewComponent("CustomerOrder", Cord);
                }


            }
            return ViewComponent("ProductManage");
        }
        //===================================客戶取消訂單光箱================================

        public IActionResult OrderCancel(int id)
        {
            OrderCancel idd = new OrderCancel();
            idd.OrderId = id;
            return View(idd);
        }



        [HttpPost]
        public IActionResult OrderCancel(COrderCancel inD)
        {
            _PlanetTravelContext.Orders.ToList();
            _PlanetTravelContext.OrderStatuses.ToList();
            OrderCancel CC = new OrderCancel();
            Order cord = _PlanetTravelContext.Orders.FirstOrDefault(a => a.OrderId == inD.OrderId);

            if (inD != null)
            {


                CC.Titel = inD.Titel;
                CC.CancaelContent = inD.CancaelContent;
                CC.OrderId = inD.OrderId;
                CC = inD.ordercancel;
                cord.OrderStatusId = 1;
                _PlanetTravelContext.OrderCancels.Add(CC);
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            };
            return Json(new { Res = false, Msg = "失敗" });
        }


        //===================================註冊頁面=======================================
        [AllowAnonymous]
        public IActionResult Createmember()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Createmember(CMemberView CCC)//註冊
        {
            if (ModelState.IsValid)
            {
                if (CCC != null)
                {

                    var e = _PlanetTravelContext.Members.Where(a => a.Email == CCC.Email).ToList();

                    if (e.Count > 0)
                        return Json(new { Res = false, Msg = "EMAIL帳號重複" });
                    if (e.Count == 0 && CCC.Password == CCC.Password2)
                    {
                        var q = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == CCC.Cityname);

                        CCC.CityId = q.CityId;
                        Member aa = CCC.member;

                        _PlanetTravelContext.Add(aa);
                        _PlanetTravelContext.SaveChanges();

                        return Json(new { Res = true, Msg = "成功" });
                    }
                    return Json(new { Res = false, Msg = "失敗" });



                }
            }
            return RedirectToAction("Index", "Home");
        }
        //===================================註冊頁面=======================================
        public IActionResult LoginModal()
        {
            return View();
        }

        public IActionResult CheckPas(string Email)
        {
            var e = _PlanetTravelContext.Members.Where(a => a.Email == Email).ToList();
            if (e.Count > 0)
                return Content("此帳號已存在", "text/plain");

            else
                return Content("", "text/plain");
        }
        //=========================忘記密碼============================
        public IActionResult Forgetpas()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Forgetpas(CLogin Mem)
        {


            Member Cust = _PlanetTravelContext.Members.FirstOrDefault(a => a.Email == Mem.Email);
            if (Cust == null)
            {
                return Json(new { Res = false });
            }
            // 取出會員信箱
            string UserEmail = Cust.Email;


            MimeMessage message = new MimeMessage();
            BodyBuilder builder = new BodyBuilder();

            Random ran = new Random();
            int rannum = ran.Next(9999) + 54785982;
            HttpContext.Session.SetInt32(CDictionary.SK_忘記密碼驗證碼, rannum);
            builder.HtmlBody = $"<p>你好，您的新密碼為{rannum}</p>" +

                              $"<div style='border: 2px solid black;text-align: center;'>      </div>" +
                              $"<p>傳送時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>";

            Cust.Password = rannum.ToString();
            _PlanetTravelContext.SaveChanges();
            message.From.Add(new MailboxAddress("PlanetTraveler星球旅遊", "planettravelermsit143@outlook.com"));
            message.To.Add(new MailboxAddress("親愛的顧客", Mem.Email));
            message.Subject = "PlanetTraveler星球旅遊:忘記密碼";
            message.Body = builder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.outlook.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("planettravelermsit143@outlook.com", "gogo1116");
                client.Send(message);
                client.Disconnect(true);
            }
            return Json(new { Res = true });

        }

        public IActionResult Resetpas(string email)
        {


            return View(email);
        }

        //==========================================在網頁加到我的最愛==================================
        public IActionResult toMyFavorites(CtoMyFavorites PMID)
        {
            if (PMID != null)
            {
                var Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == PMID.MembersId);
                if (Cus != null)
                {

                    var cc = _PlanetTravelContext.Myfavorites.Where(s => s.MembersId == PMID.MembersId && s.TravelProductId == PMID.TravelProductId).ToList();

                    if (cc.Count > 0)
                    {
                        return Json(new { Res = false });
                    }
                    Myfavorite fa = PMID.myfavorite;
                    _PlanetTravelContext.Add(fa);
                    _PlanetTravelContext.SaveChanges();
                    return Json(new { Res = true });
                }
                return Json(new { Res = false });
            }
            return RedirectToAction("Index", "Home");
        }
        //==============================檢視會員中心的我最愛==============================
        public IActionResult LMyFavorites(int MembersId, int id)
        {
            if (MembersId != 0 || id != 0)
            {
                var q = _PlanetTravelContext.Myfavorites.Where(a => a.MembersId == MembersId).Count();
                會員中心檢視最愛 CP = new 會員中心檢視最愛();
                CP.商品列表 = (from fa in _PlanetTravelContext.Myfavorites.Where(a => a.MembersId == MembersId)
                           from pid in _PlanetTravelContext.TravelProducts.Where(c => c.TravelProductId == fa.TravelProductId)
                               //from image in _PlanetTravelContext.TravelPictures.Where(m => m.TravelPictureId == pid.TravelProductId)
                           select new 最愛商品
                           {
                               TravelProductId = pid.TravelProductId,
                               TravelProductName = pid.TravelProductName,
                               MyfavoritesID = fa.MyfavoritesId,
                               Price = pid.Price,
                               TravelPicturePath = pid.TravelPictures.FirstOrDefault().TravelPicture1,
                               MembersId = MembersId,
                               myfcount = q,
                           }
                         ).ToList();

                if (id != 0)
                {
                    var c = _PlanetTravelContext.Myfavorites.FirstOrDefault(a => a.MyfavoritesId == id);
                    _PlanetTravelContext.Remove(c);
                    _PlanetTravelContext.SaveChanges();

                }


                if (CP.商品列表.Count > 0)
                {
                    return ViewComponent("MyFavorites", CP);
                }
            }

            return PartialView("Myfavorites");
        }
        public IActionResult Fcount(int MembersId)
        {
            var q = _PlanetTravelContext.Myfavorites.Where(a => a.MembersId == MembersId).Count().ToString();
            return Content(q, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Ccount(int MembersId)
        {
            var cl = _PlanetTravelContext.CouponLists.ToList();

            var 時間 = DateTime.Now.AddDays(-1).ToShortDateString();
            //var c = _PlanetTravelContext.Coupons.Where(a => DateTime.Parse(a.ExDate) > DateTime.Parse(時間)).ToList();

            //var c = cl.Where(a => a.MembersId == MembersId && a.CouponStatus == true && Convert.ToDateTime(a.Coupon.ExDate) > Convert.ToDateTime(時間)).Count().ToString();

           var q= _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId && a.CouponStatus == true).Count().ToString();


            return Content(q, "text/plain", System.Text.Encoding.UTF8);
        }
        //============================================================================
        [AllowAnonymous]
        public IActionResult City(int id)
        {
            /* var qq = _PlanetTravelContext.Members.FirstOrDefault(p=>p.MembersId==id).CityId;*///找到這個MEMBER的城市ID
            var city = _PlanetTravelContext.Cities.Where(a => a.CityId != id && a.CountryId == 1).Select(a => a.CityName);
            return Json(city);
        }


        //客戶評論檢視頁================================================

        public IActionResult CommentList(int MembersId)
        {
            if (MembersId != 0)
            {

                CCommentList Comli = new CCommentList();
                Comli.評論 = (from li in _PlanetTravelContext.Comments.Where(a => a.MembersId == MembersId)
                            select new 會員評論
                            {
                                CommentID = li.CommentId,
                                產品名稱 = li.TravelProduct.TravelProductName,
                                內容 = li.CommentText,
                                分數 = li.Star,
                                日期 = DateTime.Parse(li.CommentDate).ToString("yyyy-MM-dd"),

                                //DateTime.Parse(od.OrderDate).ToString("yyyy-MM-dd"),
                            }).ToList();
                if (Comli.評論.Count > 0)
                {
                    return ViewComponent("CommentList", Comli);
                }

            }
            return PartialView("Review");
        }
        //新增評論=============================

        public IActionResult CommentCreate(int id)
        {
            C新增評論 cc = new C新增評論();
            cc = (from a in _PlanetTravelContext.Orders.Where(s => s.OrderId == id)
                  from b in _PlanetTravelContext.Members.Where(d => d.MembersId == a.MembersId)
                  from c in _PlanetTravelContext.OrderDetails.Where(e => e.OrderId == a.OrderId)
                  select new C新增評論
                  {
                      OrderId = id,
                      CMembersId = b.MembersId,
                      CTravelProductID = c.TravelProductId,
                  }).FirstOrDefault();

            return View(cc);
        }
        [HttpPost]
        public IActionResult CommentCreate(Ccomment comm)
        {
            var q = _PlanetTravelContext.Members.Where(a => a.MembersId == comm.MembersId).Select(a => a.MemberStatusId).FirstOrDefault();
            if (q == 2)
            {
                return Json(new { Res = false });
            }
            comm.CommentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            comm.CommentStatus = true;
            Comment ss = comm.comment;
            _PlanetTravelContext.Comments.Add(ss);
            _PlanetTravelContext.SaveChanges();

            return Json(new { Res = true });
        }
        //評論修改檢視頁
        public IActionResult CommentEdit(int? CommentID)
        {
            if (CommentID != 0)
            {
                CCommentList Comli = new CCommentList();
                Comli.評論 = (from li in _PlanetTravelContext.Comments.Where(a => a.CommentId == CommentID)
                            select new 會員評論
                            {
                                CommentID = li.CommentId,
                                產品名稱 = li.TravelProduct.TravelProductName,
                                內容 = li.CommentText,
                                分數 = li.Star,

                            }).ToList();
                return View(Comli);
            }
            return RedirectToAction("CommentList");
        }
        //評論修改
        [HttpPost]
        public IActionResult CommentEdit(CCommentList inComli)
        {
            var data = _PlanetTravelContext.Comments.FirstOrDefault(a => a.CommentId == inComli.CommentID);
            if (data != null)
            {
                data.CommentText = inComli.內容;
                data.Star = inComli.分數;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            };
            return Json(new { Res = false, Msg = "失敗" });
        }
        public IActionResult CommentDelete(int MembersId, int CommentID)
        {
            if (MembersId != 0)
            {
                if (CommentID != 0)
                {
                    var od = _PlanetTravelContext.Comments.Where(x => x.CommentId == CommentID).FirstOrDefault();
                    _PlanetTravelContext.Comments.Remove(od);
                    _PlanetTravelContext.SaveChanges();
                    CCommentList Comli = new CCommentList();
                    Comli.評論 = (from li in _PlanetTravelContext.Comments.Where(a => a.MembersId == MembersId)
                                select new 會員評論
                                {
                                    CommentID = li.CommentId,
                                    產品名稱 = li.TravelProduct.TravelProductName,
                                    內容 = li.CommentText,
                                    分數 = li.Star,
                                    日期 = li.CommentDate
                                }).ToList();
                    if (Comli.評論.Count > 0)
                    {
                        return ViewComponent("CommentList", Comli);
                    }
                }

            }
            return PartialView("Review");

            //客戶評論 END================================================
        }
        public IActionResult te()
        {

            return View();
        }
        [HttpPost]
        public IActionResult te(CCsendmailcs ms)
        {
            CCsendmailcs 商品描述 = new CCsendmailcs();
            var em = _PlanetTravelContext.Members.ToList();
            var myf = _PlanetTravelContext.Myfavorites.Where(a => a.TravelProductId == ms.ProductID).Select(a => a.Members.Email).ToList();
            商品描述 = (from a in _PlanetTravelContext.TravelProducts.Where(a => a.TravelProductId == ms.ProductID)
                    select new CCsendmailcs
                    {
                        商品名稱 = a.TravelProductName,
                        商品照片 = a.TravelPictures.Where(a => a.TravelProductId == ms.ProductID).Select(a => a.TravelPicture1).FirstOrDefault(),
                        商品價格 = a.Price,
                        商品描述 = a.EventIntroduction,
                        商品詳情 = a.Description,
                    }).FirstOrDefault();

            MimeMessage message = new MimeMessage();
            BodyBuilder builder = new BodyBuilder();
            var mu = new Multipart();
            string imgPath = _enviro.WebRootPath + "/Images/logoRefer.png";

            var image = builder.LinkedResources.Add(imgPath);

            image.ContentId = MimeUtils.GenerateMessageId();

            var attach = new MimePart("image", "PNG")
            {
                FileName = Path.GetFullPath(imgPath)
            };

            builder.HtmlBody =
                                $"<img width:80px src='cid:{image.ContentId}'/>" +
                               $"<p>您好，您關注的商品更新了!</p>" +
                                $"<div class='container col-md-12'>" +
                                $"<a href='https://localhost:44302/shopping/List?TravelProductId={ms.ProductID}'>" +
                                 $"<h3>{商品描述.商品名稱}</h3>" + $"</a>" +
                                  $"<img src='https://localhost:44338/images/TravelProductPictures/{商品描述.商品照片}'width='600px''>" +
                                  $"<p>{商品描述.商品描述}</p>" +
                                  $"<p>{商品描述.商品詳情}</p>" +
                                  $"<p style='red'> 驚喜價格:{商品描述.商品價格.ToString("###,0")}元!</p>" +
                                $"</div>" +
                              $"<div style='border: 2px solid black;text-align: center;'>      </div>" +
                              $"<p>傳送時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>";

            message.From.Add(new MailboxAddress("PlanetTraveler星球旅遊", "planetmait143-1@outlook.com"));
            foreach (var item in myf)
            {
                message.To.Add(new MailboxAddress("", item));
            }

            message.Subject = "PlanetTraveler星球旅遊";
            message.Body = builder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.outlook.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("planetmait143-1@outlook.com", "gogo1116");
                client.Send(message);
                client.Disconnect(true);
            }
            return Json(new { Res = true });

        }



    }

}

