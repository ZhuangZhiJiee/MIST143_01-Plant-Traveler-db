using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CorderItemView
    {
        public int 訂單編號 { get; set; }
        public decimal 購買金額 { get; set; }

        public int 數量 { get; set; }


        public decimal 小計 { get { return this.數量 * this.購買金額; } }
    }
}
