using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
        

        public IActionResult ContactCenter()
        {
            return View(); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
