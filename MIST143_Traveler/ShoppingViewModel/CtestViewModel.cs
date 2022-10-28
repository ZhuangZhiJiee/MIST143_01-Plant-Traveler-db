using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CtestViewModel
    {
        //public List<TravelProduct> TravelProductId { get; set; }
        public List<int> TravelProductId { get; set; }
        public List<int> Count { get; set; }
        public List<string> TravelProductName { get; set; }
        public List<int> Productpicture { get; set; }
        public List<decimal> Price { get; set; }
        public List<decimal> TotalPrice { get; set; }
    }


}
