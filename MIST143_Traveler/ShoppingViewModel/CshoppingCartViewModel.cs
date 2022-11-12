using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.ShoppingViewModel
{
    public class CShoppingCartViewModel
    {
        public int MembersId { get; set; }
        public string MemberName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int PaymethodId { get; set; }
        public int? CouponId { get; set; }
        public int? CouponListId { get; set; }
        public List<CShoppingCartDetailViewModel> _CShoppingCartDetailViewModel { get; set; }
        public List<CCouponViewModel> _CCouponViewModel { get; set; }
        public List<CPayViewModel> _CPayViewModel { get; set; }

    }
}
