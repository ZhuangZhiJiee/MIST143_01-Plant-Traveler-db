using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MIST143_Traveler.MViewModel;
using MIST143_Traveler.MacViewModel;

namespace MIST143_Traveler.Controllers
{
    public class TransportationPageController : Controller
        
    {
        private PlanetTravelContext ptc;

        public TransportationPageController(PlanetTravelContext q)
        {
            ptc = q;
        }
        //[HttpGet]
        //public IActionResult testHotel()
        //{
        //    return View();
            
        //}
        //[HttpPost]
        //public IActionResult testHotel(Hotel hotel)
        //{
        //    Hotel _hotel = ptc.Hotels.Find(hotel.HotelId);

        //    return View(_hotel);

        //}
        //public IActionResult testTravelPic()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult testTravelPic(TravelPicture travelPicture)
        //{
        //    TravelPicture _travelPicture = ptc.TravelPictures.Find(travelPicture.TravelPictureId);

        //    return View(_travelPicture);
        //}
        //public IActionResult Index()
        //{
        //    var datas = from p in ptc.TravelProducts
        //                where p.Country.CountryName=="台灣"
        //                select p;
        //    return View(datas);
        //}
        public IActionResult TransportationHomePage()
        {
            return View();
            //1234
        }
        [HttpPost]
        public IActionResult TransportationHomePage(string keyword/*CLocationKeyWordViewModel 參數名稱*/)
        {
            //List<TravelProduct> _travelProduct = ptc.TravelProducts.Where(p=>p.Country.CountryName== keyword).ToList();
            vViewModel prod = ptc.TravelProducts.Select(s => new vViewModel
            {
                _ViewDetailViewModel = ptc.TravelProducts.Where(p => p.Country.CountryName.Contains(keyword)).Select(p => new ViewDetailViewModel
                {
                    TravelProductId = p.TravelProductId,
                    TravelProductName = p.TravelProductName,
                    Description = p.Description,
                    TravelPicture1 = p.TravelPictures.FirstOrDefault(a => a.TravelProductId == a.TravelProduct.TravelProductId).TravelPicture1,
                }).ToList(),
            }).FirstOrDefault();

            return View("testt", prod);
            //1234
        }
        //public IActionResult showTransportationHomePage(List<TravelProduct> _travelProduct)
        //{
        //    //寫一串LINQ查詢資料庫
        //    //回傳查詢出來的結果到view
        //    return View(_travelProduct);
        //}
        public IActionResult testt(List<TravelProduct> _travelProduct)
        {
            //寫一串LINQ查詢資料庫
            //回傳查詢出來的結果到view
            return View(_travelProduct);
        }
    }
}
