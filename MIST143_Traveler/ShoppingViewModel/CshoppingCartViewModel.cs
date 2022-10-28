using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CshoppingCartViewModel
    {
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Productpicture { get; set; }
        public TravelProduct product { get; set; }
        public List <CShoppingCartDetailViewModel> _CShoppingCartDetailViewModel { get; set; }
        
    }
}
