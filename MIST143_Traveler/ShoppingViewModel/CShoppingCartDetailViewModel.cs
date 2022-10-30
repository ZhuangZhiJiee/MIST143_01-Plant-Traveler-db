using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CShoppingCartDetailViewModel
    {
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get { return this.Count * this.Price; } }
        public string Productpicture { get; set; }
    }


}
