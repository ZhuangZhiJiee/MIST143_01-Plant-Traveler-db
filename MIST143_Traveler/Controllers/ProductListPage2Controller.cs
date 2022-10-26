using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.MViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    public class ProductListPage2Controller : Controller
    {
        private readonly PlanetTravelContext _planet;
        public ProductListPage2Controller(PlanetTravelContext planet)
        {
            _planet = planet;
            _planet.TravelPictures.ToList();
            _planet.TravelProducts.ToList();
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult longeee(int? TravelProductId) {
            Cproductlist asd = new Cproductlist();
            asd.productpicture = _planet.TravelPictures.Where(p => p.TravelProductId == TravelProductId).Select(p => p.TravelPicture1).ToList();
            return ViewComponent("Productlistpagi",asd);
        
        }
        public IActionResult ProductListPage(string keyword)
        {
            ////  string keyword = Request.Form["txtkeyword"];
            //  IEnumerable<TravelProduct> datas = null;
            //  if (string.IsNullOrEmpty(keyword))
            //  {
            //      keyword= "ProductListPage";
            //      return View(keyword);
            //  }
            //  else
            //  {
            //      datas =( from i in _planet.TravelProducts
            //              where i.TravelProductId == Convert.ToInt32(keyword) || i.TravelProductName.Contains(keyword) || i.Description.Contains(keyword)
            //              select i).ToList();
            Cproductlist plist = new Cproductlist();
            var q = (from p in _planet.TravelProducts
                     where p.TravelProductName.Contains(keyword) || p.Description.Contains(keyword)
                     select p);/*.ToList();*/
            //plist.travelProduct = q;
            return View(q.ToList());

        }
        public IActionResult filter(string[] keyword,int number)
        {
            //Cproductlist suisei = new Cproductlist();
            //suisei.隨便啦 = (from p in _planet.TravelProducts
            //              where p.TravelProductName == (string)keyword || p.Description == (string)keyword
            //              select new 好麻煩
            //              {
            //                  TravelProductName = p.TravelProductName,
            //                  TravelProductId = p.TravelProductId,
            //                  Price=p.Price,
            //                  Description=p.Description,
            //                  TravelPicture1=p.TravelPictures.FirstOrDefault().TravelPicture1,Date=p.TravelProductDetails.FirstOrDefault().Date,
            //              }).ToList();
            //if (suisei.隨便啦.Count() <= 0)
            //{
            //    suisei.隨便啦 = from p in _planet.TravelProducts
            //                 select p;

            //}
            //ViewBag.Count = suisei.隨便啦.Count();
            //return ViewComponent("Productlistpagi",suisei.隨便啦);
           

            var a =new List<TravelProduct>();
            if (keyword.Length != 0)
            {
                if (number == 0)
                {
                    foreach (var item in keyword)
                    {

                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) || p.Description.Contains(item)
                                select p;
                        a.AddRange(q);
                    }
                }
               else if(number==1)
                {
                    foreach (var item in keyword)
                    {
                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) || p.Description.Contains(item)
                                orderby p.Price
                                select p;
                        a.AddRange(q);
                    }
                }
                else if (number == 2)
                {
                    foreach (var item in keyword)
                    {
                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) || p.Description.Contains(item)
                                orderby p.Stocks
                                select p;
                        a.AddRange(q);
                    }
                }
                else if (number == 3)
                {
                    foreach (var item in keyword)
                    {
                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) || p.Description.Contains(item)
                                orderby p.TravelProductId descending
                                select p;
                        a.AddRange(q);
                    }
                }
                
               


            }
 
            ViewBag.Count = a.Count();
            //return View();
            return ViewComponent("Productlistpagi", a);
        }
        public IActionResult selecter(string date)
        {
            //var q = (from p in _planet.TravelProductDetails
            //        where p.Date == date
            //        select p).ToList();
            //return View(q);
            //Cproductlist datas = null;
            var a = DateTime.Parse(date);
            //var z = DateTime.Parse(date2);
            var qw = _planet.TravelProducts.ToList();
            var qq =_planet.TravelProductDetails.ToList()
                .Where(w =>Convert.ToDateTime(w.Date) > a).Select(q=>q.TravelProduct).Distinct().ToList();


            return ViewComponent("Productlistpagi",qq);
        }
    }
}
