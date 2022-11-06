using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly PlanetTravelContext _PlanetTravelContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, PlanetTravelContext PlanetTrave)
        {
            _PlanetTravelContext = PlanetTrave;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); //1008 push測試
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Header_Search_Bar()
        //{
        //    return RedirectToAction("ProductListHomePage", "ProductListPage", null);
        //}
        

        public IActionResult ContactCenter(int MembersId)
        {

            
            return View(MembersId); 
        }

        public IActionResult CouponHome()
        {
            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
            var q = _PlanetTravelContext.Coupons.Select(a => a).ToList();

            CCouponCMid 券 = new CCouponCMid()
            {
                已領過 = _PlanetTravelContext.CouponLists.Where(a => a.MembersId == v.MembersId).ToList(),
                所有 = q.Where(a => DateTime.Parse(a.ExDate) > DateTime.Now && a.Useful == true).ToList(),
            };
            


            return View(券);
        }

        [HttpPost]
        public IActionResult CouponHome(int MembersId)
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

                    return View(ex);
                }
            }
            return View(Coup);
        }

        public IActionResult Couponget(int id)
        {
            
                var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
                var v = JsonSerializer.Deserialize<Member>(Name);
            
            if (id != 0)
            {
                
                var q = _PlanetTravelContext.Members.Where(a => a.MembersId == v.MembersId).FirstOrDefault();
                var c = _PlanetTravelContext.CouponLists.FirstOrDefault(a => a.MembersId == q.MembersId && a.CouponId == id);
                if (c != null)
                {
                    return Json(new { Res = false });
                }
                var 優惠券 = _PlanetTravelContext.Coupons.FirstOrDefault(a => a.CouponId == id);
                if (c == null)
                {
                    CouponList li = new CouponList()
                    {
                        CouponId = 優惠券.CouponId,
                        MembersId = q.MembersId,
                        CouponStatus = true
                    };
                    _PlanetTravelContext.CouponLists.Add(li);
                    _PlanetTravelContext.SaveChanges();
                    return Json(new { Res = true });
                }
              
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
