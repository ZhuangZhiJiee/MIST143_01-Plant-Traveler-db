﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.Models.miViewModel;
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
                    return RedirectToAction("List");
                }
                else
                {
                    return Content("0", "text/plain");
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


        public IActionResult Createmember(string Email)
        {
            
            return View();
        }
        public IActionResult LoginModal()
        {
            return View();
        }


        public IActionResult Forgetpas()
        {
            return View();
        }
        
       public IActionResult LogApi(string Email)
        {
            PlanetTravelContext db = new PlanetTravelContext();
            var qq = _PlanetTravelContext.Members.Where(a => a.Email == Email).ToList();
            if (qq.Count > 0)

                return Content("此帳號已存在", "text/plain", System.Text.Encoding.UTF8);

            else

                return Content("帳號可以使用", "text/plain", System.Text.Encoding.UTF8);

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

