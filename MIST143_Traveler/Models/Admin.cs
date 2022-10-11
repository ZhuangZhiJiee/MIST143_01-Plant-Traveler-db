using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Admin
    {
        public Admin()
        {
            AdminStatuses = new HashSet<AdminStatus>();
        }

        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AdminStatus> AdminStatuses { get; set; }
    }
}
