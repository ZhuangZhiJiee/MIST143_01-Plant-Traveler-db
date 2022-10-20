using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CPasswordChange
    {
        public int MembersId { get; set; }
        public string 舊密碼 { get; set; }
        public string 新密碼 { get; set; }
        public string 確認新密碼 { get; set; }
    }
}
