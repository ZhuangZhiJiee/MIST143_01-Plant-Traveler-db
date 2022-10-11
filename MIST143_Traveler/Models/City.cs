using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class City
    {
        public City()
        {
            Hotels = new HashSet<Hotel>();
            Members = new HashSet<Member>();
            Views = new HashSet<View>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
