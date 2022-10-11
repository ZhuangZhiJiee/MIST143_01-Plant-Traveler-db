using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class Member
    {
        public Member()
        {
            MemberMessages = new HashSet<MemberMessage>();
            Orders = new HashSet<Order>();
        }

        public int MembersId { get; set; }
        public string MemberName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhotoPath { get; set; }
        public int MemberStatusId { get; set; }

        public virtual City City { get; set; }
        public virtual MemberStatus MemberStatus { get; set; }
        public virtual ICollection<MemberMessage> MemberMessages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
