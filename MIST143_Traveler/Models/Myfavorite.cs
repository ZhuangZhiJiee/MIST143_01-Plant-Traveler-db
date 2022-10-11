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
    }
}
