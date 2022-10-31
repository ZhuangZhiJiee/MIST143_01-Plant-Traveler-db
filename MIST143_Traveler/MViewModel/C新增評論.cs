using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class C新增評論
    {
        public int CTravelProductID { get; set; }
        public int CMembersId { get; set; }
        public int OrderId { get; set; }
        public string CommentText { get; set; }
        public int Star { get; set; }
        public string CommentDate { get; set; }

    }
}
