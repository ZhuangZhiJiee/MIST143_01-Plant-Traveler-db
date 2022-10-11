using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class TravelPicture
    {
        public int TravelPictureId { get; set; }
        public string TravelPicture1 { get; set; }
        public string TravelPictureText { get; set; }
        public int TravelProductId { get; set; }

        public virtual TravelProduct TravelProduct { get; set; }
    }
}
