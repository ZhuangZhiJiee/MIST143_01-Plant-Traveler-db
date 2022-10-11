using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
