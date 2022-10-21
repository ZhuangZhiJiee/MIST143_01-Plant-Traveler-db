using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class COrderDetail
    {

        public OrderDetail OrderDetail { get; set; }

        public int orderId { get; set; }

        public int 訂單編號 { get; set; }

        public List<string> 商品名稱 { get; set; }
        public List<string> 訂購日期 { get; set; }

        public List<decimal> 購買金額 { get; set; }
        public List<decimal> 訂單狀態 { get; set; }
    }
    
}

