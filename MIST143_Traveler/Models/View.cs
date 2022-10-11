using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class View
    {
        public int ViewId { get; set; }
        public string ViewName { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
