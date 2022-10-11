using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class AdminStatus
    {
        public int AdminStatusId { get; set; }
        public int AdminId { get; set; }
        public bool CommentStatus { get; set; }
        public bool ProductStatus { get; set; }
        public bool MemberStatus { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
