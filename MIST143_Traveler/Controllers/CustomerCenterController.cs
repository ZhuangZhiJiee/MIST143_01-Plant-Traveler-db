using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.Models.miViewModel;

using MIST143_Traveler.MViewModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class CustomerCenterController : Controller
    {
        private readonly PlanetTravelContext _PlanetTravelContext;
        private IWebHostEnvironment _enviro;
        public CustomerCenterController(PlanetTravelContext PlanetTrave, IWebHostEnvironment p)
        {
            _PlanetTravelContext = PlanetTrave;
            _enviro = p;
        }

        public IActionResult Index()
        {
            return PartialView();
        }


        public IActionResult newLoginpag()
        {
            return View();
        }
        //會員中心變更保存資料
        [HttpPost]
        public IActionResult SaveCusInfo(CMemberView inCus)
        {
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCus.MembersId);

            if (Cus != null)
            {

                //Cus.CityId = ccc.CityId;
                Cus.Address = inCus.Address;
                //Cus.Password = inCus.Password;
                Cus.MemberName = inCus.MemberName;
                Cus.Phone = inCus.Phone;
                Cus.CityId = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == inCus.Cityname).CityId;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
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

                }
            }
            return ViewComponent("PasswordChange");
        }
        //=============================變更密碼頁============================


        public IActionResult List(int MembersId)
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


        //左邊功能開始
        [HttpPost]
        public IActionResult newLoginpag(CLogin vModel)
        {
            Member cust = new PlanetTravelContext().Members.FirstOrDefault
              (c => c.Email.Equals(vModel.Email));
            if (cust != null)
            {

                if (cust.Password.Equals(vModel.Password))
                {
                    string jsonUser = JsonSerializer.Serialize(cust);
                    HttpContext.Session.SetString(
                        CDictionary.SK_Login, jsonUser);

                    return Json(new { Res = true, Msg = "成功" });
                }
                else
                    return Json(new { Res = false, Msg = "失敗" });

            }
            return View();
        }



        public IActionResult Order(int MembersId, int? OrderId)//客戶找出訂單
        {
            if (MembersId != 0)
            {
                if (OrderId != null)
                {
                    var od = _PlanetTravelContext.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
                    //_PlanetTravelContext.Orders.Remove(od);
                    od.OrderStatusId = 4;
                    _PlanetTravelContext.SaveChanges();
                }
                COrderDetail Oe = new COrderDetail();

                Oe.訂單 = (from c in _PlanetTravelContext.Orders.Where(a => a.MembersId == MembersId)
                         select new 會員訂單
                         {
                             orderId = c.OrderId,
                             訂單編號 = c.OrderId,
                             商品名稱 = c.OrderDetails.FirstOrDefault().TravelProduct.TravelProductName,
                             訂購日期 = c.OrderDate,
                             訂單狀態 = c.OrderStatus.OrderStatusName,
                             購買金額 = c.OrderDetails.FirstOrDefault().UnitPrice,

                         }).ToList();

                if (Oe.訂單.Count > 0)
                {
                    return ViewComponent("CustomerOrder", Oe);
                }

            }
            return ViewComponent("ProductManage");
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

                //Cus.CityId = ccc.CityId;
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

        public IActionResult Createmember()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Createmember(CMemberView CCC)//註冊
        {
            var q = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == CCC.Cityname);

            CCC.CityId = q.CityId;
            Member aa = CCC.member;

            _PlanetTravelContext.Add(aa);
            _PlanetTravelContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
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


        public IActionResult Forgetpas()
        {
            return View();
        }


        public IActionResult City(int id)
        {
            /* var qq = _PlanetTravelContext.Members.FirstOrDefault(p=>p.MembersId==id).CityId;*///找到這個MEMBER的城市ID
            var city = _PlanetTravelContext.Cities.Where(a => a.CityId != id && a.CountryId == 1).Select(a => a.CityName);
            return Json(city);
        }


        //客戶評論================================================

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
                                日期 = li.CommentDate
                            }).ToList();
                    if (Comli.評論.Count > 0)
                    {
                        return ViewComponent("CommentList", Comli);
                    }
                
            }
            return PartialView("Review");
        }

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

          //客戶評論 ================================================
        }
    }
}

