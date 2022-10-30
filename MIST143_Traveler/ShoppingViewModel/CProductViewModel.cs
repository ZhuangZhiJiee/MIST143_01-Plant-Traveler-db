﻿using MIST143_Traveler.Models;
using MIST143_Traveler.ShoppingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CProductViewModel
    {
        public List<CProductDetailViewModel> _CProductDetailViewModel;
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public int Stocks { get; set; }
        public string Description { get; set; }
        public string EventIntroduction { get; set; }
        public string MapUrl { get; set; }
        public string PreparationDescription { get; set; }
        public int Count { get; set; }
        public List<string> DailyDetailText { get; set; }
        public List<string> Date { get; set; }
        public List<string> HotelName { get; set; }
        public List<TravelPicture> Productpictures { get; set; }
        public int MembersId { get; set; }
        public bool MyfavoritesID { get; set; }
    }
}
