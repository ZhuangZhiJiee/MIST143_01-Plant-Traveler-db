﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class ProductListPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductListHomePage()
        {
            return View();
        }
    }
}
