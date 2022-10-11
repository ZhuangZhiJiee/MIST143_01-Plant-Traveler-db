using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            TravelProducts = new HashSet<TravelProduct>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<TravelProduct> TravelProducts { get; set; }
    }
}
