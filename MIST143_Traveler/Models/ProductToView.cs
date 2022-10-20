using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class ProductToView
    {
        public int ProductToViewId { get; set; }
        public int TravelProductDetailId { get; set; }
        public int ViewId { get; set; }

        public virtual TravelProductDetail TravelProductDetail { get; set; }
        public virtual View View { get; set; }
    }
}
