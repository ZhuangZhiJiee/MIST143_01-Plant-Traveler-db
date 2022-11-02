using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class COrderDetailview
    {
        public List<string> FImagePath { get; set; }

        public int 訂單編號 { get; set; }
        public int TravelProductId{get; set;}
       

        public string 商品名稱 { get; set; }
        public string 訂購日期 { get; set; }

        public decimal 購買金額 { get; set; }
        public int 數量 {get; set;}
        public string 優惠券 { get; set; }

        public string 付款方式 { get; set; }

    }
}
