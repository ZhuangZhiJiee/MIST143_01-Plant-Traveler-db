using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class COrderDetail
    {
        public List<訂單管理> 訂單;

       
        
    }
    public class 訂單管理
    {
        public int TravelProductId
        {
            get;set;
        }
        public List<string> FImagePath { get; set; }
        public int orderId { get; set; }

        public int 訂單編號 { get; set; }

        public string 商品名稱 { get; set; }
        public string 訂購日期 { get; set; }

        public decimal 購買金額 { get; set; }
        public string 訂單狀態 { get; set; }

       public int 數量
        {
            get;set;
        }
        public string 優惠券 { get; set; }

        public string 付款方式 { get; set; }

        public int 評論狀態 { get; set; }

        public decimal? 折扣金額{ get; set; }

        public string 隨行人員 { get; set; }
    }
    
}

