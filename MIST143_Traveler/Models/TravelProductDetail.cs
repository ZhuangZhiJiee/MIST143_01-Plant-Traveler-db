using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class TravelProductDetail
    {
        public TravelProductDetail()
        {
            ProductToTransportations = new HashSet<ProductToTransportation>();
            ProductToViews = new HashSet<ProductToView>();
        }

        public int TravelProductDetailId { get; set; }
        public int TravelProductId { get; set; }
        public int Day { get; set; }
        public int? HotelId { get; set; }
        public string Date { get; set; }
        public string DailyDetailText { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual TravelProduct TravelProduct { get; set; }
        public virtual ICollection<ProductToTransportation> ProductToTransportations { get; set; }
        public virtual ICollection<ProductToView> ProductToViews { get; set; }
    }
}
