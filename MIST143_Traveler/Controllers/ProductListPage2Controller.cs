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
            _planet.TravelProductTypes.ToList();
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult longeee(int? TravelProductId)
        {
            Cproductlist asd = new Cproductlist();
            asd.productpicture = _planet.TravelPictures.Where(p => p.TravelProductId == TravelProductId).Select(p => p.TravelPicture1).FirstOrDefault();
            return ViewComponent("Productlistpagi", asd);

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
                     where p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架"
                     select p).ToList();
            //plist.travelProduct = q;
            ViewBag.keyword = keyword;
            return View(q);
        }
        public IActionResult getcount(string keyword)
        {
            var a = new List<Cproductlist>();
            if (keyword != null)
            {
                var q = _planet.TravelProducts.Where(x => x.ProductStatus == "已上架" && x.TravelProductName.Contains(keyword)).GroupBy(a => a.Country.CountryName).Select(a => new Cproductlist
                {
                    coutryname = a.Key,
                    count = a.Count(),
                });
                a.AddRange(q);
            }
            else
            {
                var q = _planet.TravelProducts.Where(x => x.ProductStatus == "已上架").GroupBy(a => a.Country.CountryName).Select(a => new Cproductlist
                {
                    coutryname = a.Key,
                    count = a.Count(),

                });

                a.AddRange(q);
            }

            return Json(a);
        }
        public IActionResult filter(string[] keyword, int number)
        {

            var a = new List<TravelProduct>();
            if (keyword.FirstOrDefault() != null)
            {
                if (number == 0)
                {
                    foreach (var item in keyword)
                    {

                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架"
                                select p;
                        a.AddRange(q);
                    }
                }
                else if (number == 1)
                {
                    foreach (var item in keyword)
                    {
                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架"
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
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架"
                                orderby (p.Quantity - p.Stocks) descending
                                select p;
                        a.AddRange(q);
                    }
                }
                else if (number == 3)
                {
                    foreach (var item in keyword)
                    {
                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架"
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

        public IActionResult countall(int num)
        {
            return View();
        }
        public IActionResult product(int? numid)
        {
            var a = new List<TravelProduct>();
            if (numid == 1)
            {
                var q = from p in _planet.TravelProducts
                        where p.TravelProductType.TravelProductTypeId == 1 && p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            else if (numid == 2)
            {
                var q = from p in _planet.TravelProducts
                        where p.TravelProductType.TravelProductTypeId == 2 && p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            else if (numid == 3)
            {
                var q = from p in _planet.TravelProducts
                        where p.TravelProductType.TravelProductTypeId == 3 && p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            else if (numid == 4)
            {
                var q = from p in _planet.TravelProducts
                        where p.TravelProductType.TravelProductTypeId == 4 && p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            else if (numid == 5)
            {
                var q = from p in _planet.TravelProducts
                        where p.TravelProductType.TravelProductTypeId == 5 && p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            else if (numid == 6)
            {
                var q = from p in _planet.TravelProducts
                        where p.TravelProductType.TravelProductTypeId == 6 && p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            else
            {
                var q = from p in _planet.TravelProducts
                        where p.ProductStatus == "已上架"
                        select p;
                a.AddRange(q);
            }
            return ViewComponent("Productlistpagi", a);
        }
        public IActionResult selecter(string date, string date2)
        {
            //var q = (from p in _planet.TravelProductDetails
            //        where p.Date == date
            //        select p).ToList();
            //return View(q);
            //Cproductlist datas = null;
            if (date == null || date2 == null)
            {

                return View("請輸入正確日期");
            }
            var a = DateTime.Parse(date);
            var a2 = DateTime.Parse(date2);
            //var z = DateTime.Parse(date2);
            var qw = _planet.TravelProducts.ToList();
            var qq = _planet.TravelProductDetails.ToList()
                .Where(w => Convert.ToDateTime(w.Date) > a && Convert.ToDateTime(w.Date) < a2 && w.TravelProduct.ProductStatus == "已上架").OrderBy(o => Convert.ToDateTime(o.Date)).Select(q => q.TravelProduct).Distinct().ToList();


            return ViewComponent("Productlistpagi", qq);
        }
        public IActionResult see(string contry)
        {
            //var q = (from p in _planet.TravelProductDetails
            //        where p.Date == date
            //        select p).ToList();
            //return View(q);
            //Cproductlist datas = null;
            //var z = DateTime.Parse(date2);
            if (contry == null)
            {
                var q = from p in _planet.TravelProducts
                        where p.ProductStatus == "已上架"
                        select p;
                return ViewComponent("Productlistpagi", q.Distinct().ToList());
            }
            string[] aaaa = contry.Split(",");

            var pp = _planet.TravelProducts.Where(e => aaaa.Contains(e.Country.CountryName) && e.ProductStatus == "已上架").Select(bb => bb).Distinct().ToList();

            return ViewComponent("Productlistpagi", pp);
        }
        public IActionResult kkkkk(string rest)
        {
            return Content(rest);
        }
        public IActionResult getSearchCountt(string keyword)
        {
            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架").Count();
            return Json(count);
        }


        //--------------選爆--------------------------------------------------
        public IActionResult getCountryCount(string keyword, string country)
        {
            var a = new List<TravelProduct>();
            if (keyword != null)
            {
                if (country == null)
                {
                    var q = from p in _planet.TravelProducts
                            where p.ProductStatus == "已上架" && p.TravelProductName.Contains(keyword)|| p.ProductStatus == "已上架" && p.Description.Contains(keyword)
                            select p;
                    return ViewComponent("Productlistpagi", q.Distinct().ToList());
                }
                else
                {
                    string[] aaaa = country.Split(",");

                    var pp = _planet.TravelProducts.Where(e => aaaa.Contains(e.Country.CountryName) && e.ProductStatus == "已上架" && e.TravelProductName.Contains(keyword)||e.Description.Contains(keyword) && aaaa.Contains(e.Country.CountryName) && e.ProductStatus == "已上架").Select(bb => bb);
                 a.AddRange(pp.Distinct().ToList());
                }
               

            }
            else { 
            if (country != null)
            {
               string[] aaa = country.Split(",");

            var pp = _planet.TravelProducts.Where(e => aaa.Contains(e.Country.CountryName) && e.ProductStatus == "已上架" && e.TravelProductName.Contains(keyword) || e.Description.Contains(keyword) && aaa.Contains(e.Country.CountryName) && e.ProductStatus == "已上架").Select(bb => bb);
                a.AddRange(pp.Distinct().ToList());
            }
            }
            return ViewComponent("Productlistpagi", a);

        }
        public IActionResult productkey(string keyword, int numidkey) 
        {
            //if (keyword != null)
            //{
            //    if (numidkey == 1)
            //    { 
            //    var q =_planet.TravelProducts.Where(d=>d.TravelProductTypeId==1 && keyword.Contains(d.TravelProductName)||d.Description.Contains(keyword)&&)
            //    }
            //}

            return View();
        }
    }

}
