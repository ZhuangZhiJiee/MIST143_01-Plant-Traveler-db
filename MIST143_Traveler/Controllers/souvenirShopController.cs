using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class souvenirShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
