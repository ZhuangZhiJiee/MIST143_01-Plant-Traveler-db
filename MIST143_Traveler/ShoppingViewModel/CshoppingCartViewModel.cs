using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CshoppingCartViewModel
    {
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Productpicture { get; set; }
        public TravelProduct product { get; set; }
        public CtestViewModel _CtestViewModel { get; set; }
    }
}
