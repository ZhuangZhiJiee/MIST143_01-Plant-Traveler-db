using MIST143_Traveler.Models;
using MIST143_Traveler.ShoppingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CProductViewModel
    {
        //public List<產品格式> 產品列表;
        public List<CProductDetailViewModel> _CProductDetailViewModel;
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public int Stocks { get; set; }
        public string Description { get; set; }
        public string EventIntroduction { get; set; }
        public string MapUrl { get; set; }
        public string PreparationDescription { get; set; }
        public int count { get; set; }
        public bool MyfavoritesID
        {
            get;
            set;
        }
        public int MembersId { get; set; }
        public List<TravelPicture> productpictures { get; set; }
        public List<string> DailyDetailText { get; set; }
        public List<string> Date { get; set; }
        public List<string> HotelName { get; set; }
    }
    
    public class 產品格式
    {
        
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public int TravelProductTypeId { get; set; }
        public int Stocks { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int Cost { get; set; }
        public string EventIntroduction { get; set; }
        public string MapUrl { get; set; }
        public string PreparationDescription { get; set; }
        public int count { get; set; }
        public List<TravelPicture> productpictures { get; set; }
        public List<TravelPicture> Runproductpictures { get; set; }
        public List<string> DailyDetailText { get; set; }
        public string FImagePath { get; set; }
        public List<string> Date { get; set; }
        //public List<string> View { get; set; }
        public List<string> HotelName { get; set; }
    }
}
