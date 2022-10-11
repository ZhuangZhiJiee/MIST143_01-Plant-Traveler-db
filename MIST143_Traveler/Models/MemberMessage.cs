using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class MemberMessage
    {
        public int MemberMessageId { get; set; }
        public int MembersId { get; set; }
        public string MemberMessageData { get; set; }

        public virtual Member Members { get; set; }
    }
}
