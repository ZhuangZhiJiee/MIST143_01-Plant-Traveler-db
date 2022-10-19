using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class COrderDetail
    {
        public List<會員訂單> 訂單;
    }
    public class 會員訂單
    {

      
        public int orderId { get; set; }

        public int 訂單編號 { get; set; }

        public string 商品名稱 { get; set; }
        public string 訂購日期 { get; set; }

        public decimal 購買金額 { get; set; }
        public string 訂單狀態 { get; set; }
    }


}

