using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class TravelProductType
    {
        public TravelProductType()
        {
            TravelProducts = new HashSet<TravelProduct>();
        }

        public int TravelProductTypeId { get; set; }
        public string TravelProductTypeName { get; set; }

        public virtual ICollection<TravelProduct> TravelProducts { get; set; }
    }
}
