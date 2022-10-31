using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CCommentList
    {
        
        public List<會員評論> 評論;
        public string 內容 { get; set; }
        public int 分數 { get; set; }

        public int CommentID { get; set; }
        
    }
   

    public class 會員評論
    {

        public int CommentID { get; set; }

        public string 產品名稱 { get; set; }

        public string 內容 { get; set; }
        public int 分數 { get; set; }

        public string 日期 { get; set; }
        
    }
}
