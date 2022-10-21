using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CProductMemberViewModel
    {
        public List<產品會員> 產品會員;

    }
    public class 產品會員
    {
        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public List<TravelPicture> productpictures { get; set; }
        public int MembersID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Count { get; set; }
    }
}

