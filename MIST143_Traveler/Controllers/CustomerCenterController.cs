﻿using Microsoft.AspNetCore.Http;
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
        public CustomerCenterController(PlanetTravelContext PlanetTrave)
        {
            _PlanetTravelContext = PlanetTrave;
        }
        public IActionResult Index()
        {
            return PartialView();
        }
        public IActionResult newLoginpag()
        {
            return View();
        }
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
                    return RedirectToAction("index", "home");
                    //return Content("123", "text/plain", System.Text.Encoding.UTF8);
                    //return Json("成功");
                }
            
            }

            return View();

        }
        public IActionResult List()
        {
            //PlanetTravelContext db = new PlanetTravelContext();//測試一下

            return View();
        }
        //左邊功能開始
        public IActionResult ProductManage()
        {
            return PartialView();
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
       
        public IActionResult CustomerInfo(Member memberid)
        {
            IEnumerable<Member> datas = null;
          
                datas = _PlanetTravelContext.Members.Where(a => a.Email == memberid.Email);
            
            return PartialView(datas);
        }
        [HttpPost]
        public IActionResult CustomerInfo()
        {
            //_PlanetTravelContext.Members
            return PartialView();
        }
        //左邊功能結束


        public IActionResult Createmember()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Createmember(CMemberView CCC)
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

