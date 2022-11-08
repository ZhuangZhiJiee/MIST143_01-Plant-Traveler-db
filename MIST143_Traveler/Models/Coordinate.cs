using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Coordinate
    {
        public Coordinate()
        {
            ProductCoordinates = new HashSet<ProductCoordinate>();
        }

        public int CoordinateId { get; set; }
        public string AttractionsName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<ProductCoordinate> ProductCoordinates { get; set; }
    }
}
