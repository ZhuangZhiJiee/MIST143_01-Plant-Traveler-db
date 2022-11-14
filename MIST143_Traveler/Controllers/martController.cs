using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.MacViewModel;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class martController : Controller
    {
        private PlanetTravelContext pt;
        public martController(PlanetTravelContext q)
        {
            pt = q;
        }
        public IActionResult CartView()
        {
            if (HttpContext.Session.Keys.Contains(CMartDictionary.SK_PURCHASED_PRODUCTS))
            {
                string jsonCart = HttpContext.Session.GetString(CMartDictionary.SK_PURCHASED_PRODUCTS);
                List<CMartCartItem> cart = JsonSerializer.Deserialize<List<CMartCartItem>>(jsonCart);
                return View(cart);
            }
            else
                return RedirectToAction("List");
        }
        public IActionResult BothAddToCart(int? id)
        {

            TProduct prod = pt.TProducts.FirstOrDefault(t => t.FId == 3);
                // TProduct prod = db.TProducts.FirstOrDefault(t => t.FId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);

        }
        [HttpPost]


        public IActionResult BothAddToCart(CBothAddToCartViewModel vModel)
        {
            //dbDemoContext db = new dbDemoContext();
            TProduct prod = pt.TProducts.FirstOrDefault(t => t.FId == vModel.txtFid);
            if (prod == null)
                return RedirectToAction("List");
            string jsonCart = "";
            List<CMartCartItem> list = null;
            if (!HttpContext.Session.Keys.Contains(CMartDictionary.SK_PURCHASED_PRODUCTS))
                list = new List<CMartCartItem>();
            else
            {
                jsonCart = HttpContext.Session.GetString(CMartDictionary.SK_PURCHASED_PRODUCTS);
                list = JsonSerializer.Deserialize<List<CMartCartItem>>(jsonCart);
            }
            CMartCartItem item = new CMartCartItem()
            {
                count = vModel.txtCount,
                price = (decimal)prod.FPrice,
                productId = vModel.txtFid,
                product = prod
            };
            list.Add(item);
            jsonCart = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(
                CMartDictionary.SK_PURCHASED_PRODUCTS, jsonCart);
            return RedirectToAction("CartView");

        }


    }
}


