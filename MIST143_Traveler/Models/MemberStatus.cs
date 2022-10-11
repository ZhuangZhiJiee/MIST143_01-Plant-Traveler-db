using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class MemberStatus
    {
        public MemberStatus()
        {
            Members = new HashSet<Member>();
        }

        public int MemberStatusId { get; set; }
        public string MemberStatusName { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
