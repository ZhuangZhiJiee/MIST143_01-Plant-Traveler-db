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
        public CustomerCenterController(PlanetTravelContext PlanetTrave,IWebHostEnvironment p)
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
        public IActionResult SaveCusInfo(Member inCus)
        {
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCus.MembersId);

            if (Cus != null)
            {

                //Cus.CityId = ccc.CityId;
                Cus.Address = inCus.Address;
                Cus.Password = inCus.Password;
                Cus.MemberName = inCus.MemberName;
                Cus.Phone = inCus.Phone;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }
        //[HttpPost]
        //public IActionResult SaveCusInfo(CMemberView Cus)
        //{
        //    Member aCus = _PlanetTravelContext.Members.Where(a => a.MembersId == Cus.MembersId).Select(inCus=>new CMemberView){ }

        //    if (Cus != null)
        //    {

        //        //Cus.CityId = ccc.CityId;
        //        Cus.Address = inCus.Address;
        //        Cus.Password = inCus.Password;
        //        Cus.MemberName = inCus.MemberName;
        //        Cus.Phone = inCus.Phone;
        //        _PlanetTravelContext.SaveChanges();
        //        return Json(new { Res = true, Msg = "成功" });
        //    }
        //    return Json(new { Res = false, Msg = "失敗" });
        //}
        public IActionResult List(int MembersId)
        {
            CMemberView cm =new CMemberView();
            if (MembersId != 0)
            {

                var Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == MembersId);
                //cm.MembersId = Cus.MembersId;
                //var CC = _PlanetTravelContext.Members.FirstOrDefault(x => x.MembersId == cm.MembersId);
                if (Cus != null)
                {
                    return View(Cus);
                }
            }

            return View("test");
        }

        public IActionResult TextM(int? MembersId)
        {
            CMemberView Cus = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(inCus => new CMemberView
            {
                Address = inCus.Address,
                Password = inCus.Password,
                MemberName = inCus.MemberName,
                Phone = inCus.Phone,
                FImagePath = inCus.PhotoPath,

            }).FirstOrDefault();
            //CMemberView CMCITY = new CMemberView();
            //var ccc = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == CMCITY.城市);
            //ccc.CityId = inCus.CityId;

            //Cus.CityId = ccc.CityId;

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
                    //return RedirectToAction("index", "home");
                    //return Content("123", "text/plain", System.Text.Encoding.UTF8);
                    return Json(new { Res = true, Msg = "成功" });
                }
                else
                    return Json(new { Res = false, Msg = "失敗" });

            }
            return View();
        }
   
        public IActionResult Order(int MembersId,int? OrderId)//客戶找出訂單
        {
            if (MembersId != 0)
            {
                if (OrderId != null)
                {
                    var od = _PlanetTravelContext.Orders.FirstOrDefault(x => x.OrderId == OrderId);
                    _PlanetTravelContext.Orders.Remove(od);
                    _PlanetTravelContext.SaveChanges();
                }
                
                var Cus = _PlanetTravelContext.Orders.Where(a => a.MemberId == MembersId).ToList();
                
               
                if (Cus.Count > 0)
                {
                    
                    //var od = _PlanetTravelContext.Orders.Where(o => o.MemberId == MembersId);
                    return ViewComponent("CustomerOrder",Cus);
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

            if (MembersId != 0)
            {
                var Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == MembersId);
                //var ctt = _PlanetTravelContext.Cities.FirstOrDefault(b => b.CityId == CityId);
                //CMemberView cm = new CMemberView();
                //cm.城市 = ctt.CityName;
                //Member gg = cm.member;
                if (Cus != null)
                {
                    return View(Cus);
                }
            }

            return View("test");
        }
        [HttpPost]
        public IActionResult CustomerInfo(CMemberView inCus)
        {

            var info = _PlanetTravelContext.Members.Where(a => a.Email == inCus.Email);

            var ct = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == inCus.城市);
            ct.CityId = inCus.CityId;

            return PartialView();
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
            var q = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == CCC.城市);

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


        public IActionResult City()
        {
            var city = _PlanetTravelContext.Cities.Where(a => a.CountryId == 1).Select(a => a.CityName);
            return Json(city);
        }


        //public IActionResult 訂單管理_未使用()
        //{
        //    return PartialView();
        //}
        //public IActionResult 訂單管理_已使用()
        //{
        //    return PartialView();
        //}
    }
}

