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
            _planet.Comments.ToList();
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
                     where p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架"||p.Country.CountryName == (keyword) && p.ProductStatus == "已上架"
                     select p).ToList();
            //plist.travelProduct = q;
            ViewBag.keyword = keyword;
            ViewBag.numa = q.Count();
            ViewBag.rew = "0";
            return View(q);
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
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架" || p.Country.CountryName==(item) && p.ProductStatus == "已上架" || item=="全部" && p.ProductStatus == "已上架"
                                select p;
                        a.AddRange(q);
                    }
                }
                else if (number == 1)
                {
                    foreach (var item in keyword)
                    {
                        var q = from p in _planet.TravelProducts
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架"||p.Country.CountryName==(item) && p.ProductStatus == "已上架" || item == "全部" && p.ProductStatus == "已上架"
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
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架" || p.Country.CountryName == (item) && p.ProductStatus == "已上架" || item == "全部" && p.ProductStatus == "已上架"
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
                                where p.TravelProductName.Contains(item) && p.ProductStatus == "已上架" || p.Description.Contains(item) && p.ProductStatus == "已上架" || p.Country.CountryName == (item) && p.ProductStatus == "已上架" || item == "全部" && p.ProductStatus == "已上架"
                                orderby p.TravelProductId descending
                                select p;
                        a.AddRange(q);
                    }
                }
            }
            else
            {
                if (number == 0)
                {


                    var q = from p in _planet.TravelProducts
                            where p.ProductStatus == "已上架"
                            select p;
                    a.AddRange(q);

                }
                else if (number == 1)
                {

                    var q = from p in _planet.TravelProducts
                            where p.ProductStatus == "已上架"
                            orderby p.Price
                            select p;
                    a.AddRange(q);

                }
                else if (number == 2)
                {

                    var q = from p in _planet.TravelProducts
                            where p.ProductStatus == "已上架"
                            orderby p.Stocks
                            select p;
                    a.AddRange(q);

                }
                else if (number == 3)
                {

                    var q = from p in _planet.TravelProducts
                            where p.ProductStatus == "已上架"
                            orderby p.TravelProductId descending
                            select p;
                    a.AddRange(q);

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
            var qq = _planet.TravelProductDetails.GroupBy(e => e.TravelProductId).Select(g => new
            {
                travelProductID = g.Key,
                dayCount = g.Count(),
            }).ToList();
            if (numid == 1)
            {
                var qqq = qq.Where(e => e.dayCount == 1).Select(e => e.travelProductID).ToList();
                a = _planet.TravelProducts.Where(e => qqq.Contains(e.TravelProductId) && e.ProductStatus == "已上架").ToList();
            }
            else if(numid==3)
                    {
                var qqq = _planet.TravelProducts.Where(d => d.TravelProductTypeId == 1 && d.ProductStatus == "已上架").Select(d => d).ToList();
                a.AddRange(qqq);
            }
            else if (numid == 4)
            {
                var qqq = _planet.TravelProducts.Where(d => d.TravelProductTypeId == 2 && d.ProductStatus == "已上架").Select(d => d).ToList();
                a.AddRange(qqq);
            }
            else if (numid == 5)
            {
                var qqq = _planet.TravelProducts.Where(d => d.TravelProductTypeId == 3 && d.ProductStatus == "已上架").Select(d => d).ToList();
                a.AddRange(qqq);
            }
            else if (numid == 6)
            {
                var qqq = _planet.TravelProducts.Where(d => d.TravelProductTypeId == 5 && d.ProductStatus == "已上架").Select(d => d).ToList();
                a.AddRange(qqq);
            }
            else if (numid == 7)
            {
                var qqq = _planet.TravelProducts.Where(d => d.TravelProductTypeId == 4 && d.ProductStatus == "已上架").Select(d => d).ToList();
                a.AddRange(qqq);
            }
            else if (numid == 8)
            {
                var qqq = _planet.TravelProducts.Where(d => d.TravelProductTypeId == 6 && d.ProductStatus == "已上架").Select(d => d).ToList();
                a.AddRange(qqq);
            }
            else
            {
                var qqq = qq.Where(e => e.dayCount>1).Select(e => e.travelProductID).ToList();
                a = _planet.TravelProducts.Where(e => qqq.Contains(e.TravelProductId)&&e.ProductStatus=="已上架").ToList();
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
            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架"||p.Country.CountryName==(keyword) && p.ProductStatus == "已上架"||keyword==""&& p.ProductStatus == "已上架").Count();
            return Json(count);

        }
        public IActionResult getSearchCounty(string keyword)
        {
           
            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 1|| keyword == ("台灣") && p.CountryId == 1 && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 1 || p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 2 || keyword == ("日本") && p.CountryId == 2 && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 2  || p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 8|| keyword == ("韓國")&& p.CountryId == 8&& p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 8 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 1 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 2 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 8).Count();
            return Json(count);
        }
        public IActionResult getSearchCountyt(string keyword)
        {

            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 1|| keyword == ("台灣") && p.CountryId == 1 || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 1||keyword== "" && p.ProductStatus == "已上架" && p.CountryId == 1).Count();
            return Json(count);
        }
        public IActionResult getSearchCountyj(string keyword)
        {

            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 2 || keyword == ("日本") && p.CountryId == 2 && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 2 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 2).Count();
            return Json(count);
        }
        public IActionResult getSearchCountyk(string keyword)
        {

            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 8 && keyword == ("韓國") && p.CountryId == 8 && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 8 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 8).Count();
            return Json(count);
        }
        public IActionResult getSearchCountd(string keyword)
        {          
            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 9 && keyword == ("越南") && p.CountryId == 9 && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 9 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 9).Count();
            return Json(count);
        }
        public IActionResult getSearchCountb(string keyword)
        {
            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 5 && keyword == ("美國") && p.CountryId == 5 && p.ProductStatus == "已上架" || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 5 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 5).Count();
            return Json(count);
        }
        public IActionResult getSearchCounto(string keyword)
        {
            var count = _planet.TravelProducts.Where(p => p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 6&& keyword == ("英國") || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 6 && keyword == ("英國") || p.TravelProductName.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 7 && keyword == ("法國") || p.Description.Contains(keyword) && p.ProductStatus == "已上架" && p.CountryId == 7&& keyword == ("法國") || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 6 || keyword == "" && p.ProductStatus == "已上架" && p.CountryId == 7).Count();
            return Json(count);
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

        //--------------選爆--------------------------------------------------
        public IActionResult getCountryCount(string keyword, string country)
        {
            var a = new List<TravelProduct>();
            if (keyword != null)
            {
                if (country == null)
                {
                    var q = from p in _planet.TravelProducts
                            where p.ProductStatus == "已上架" && p.TravelProductName.Contains(keyword)|| p.ProductStatus == "已上架" && p.Description.Contains(keyword)||keyword==p.Country.CountryName&&p.ProductStatus=="已上架"
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
        public IActionResult star()
        {
            var q = _planet.Comments.Where(c => c.TravelProductId == c.TravelProduct.TravelProductId).Average(d => d.Star);

        return ViewComponent("Productlistpagi", q);
        }
    }

}
