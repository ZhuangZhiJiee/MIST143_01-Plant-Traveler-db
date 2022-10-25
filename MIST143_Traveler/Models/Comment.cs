using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int MembersId { get; set; }
        public int TravelProductId { get; set; }
        public string CommentText { get; set; }
        public int Star { get; set; }
        public string CommentDate { get; set; }

        public virtual Member Members { get; set; }
        public virtual TravelProduct TravelProduct { get; set; }
    }
}
