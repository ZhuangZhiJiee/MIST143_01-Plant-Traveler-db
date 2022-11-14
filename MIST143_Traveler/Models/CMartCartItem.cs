using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Models
{
    public class CMartCartItem
    {
        public int productId { get; set; }
        [DisplayName("購買量")]
        public int count { get; set; }
        [DisplayName("採購價")]
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; } }
        public TProduct product { get; set; }
    }
}
