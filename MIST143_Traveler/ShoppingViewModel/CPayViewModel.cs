using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CPayViewModel
    {
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string AccompanyPeople { get; set; }

    }
}
