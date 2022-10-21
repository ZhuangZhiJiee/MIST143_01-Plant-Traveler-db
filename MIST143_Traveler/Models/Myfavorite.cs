using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Myfavorite
    {
        public int MyfavoritesId { get; set; }
        public int TravelProductId { get; set; }
        public int MembersId { get; set; }
        public string PhotoPath { get; set; }

        public virtual Member Members { get; set; }
        public virtual TravelProduct TravelProduct { get; set; }
    }
}
