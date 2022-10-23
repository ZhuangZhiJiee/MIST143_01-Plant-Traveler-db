using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class 會員中心檢視最愛
    {
        public List<最愛商品> 商品列表;
    }
    public class 最愛商品
    {
            public IFormFile photo { get; set; } //圖片的檔案
            public string TravelPicturePath //圖片的路徑
            {
                get;
                set;
            }
            public int TravelProductId //商品ID
            {
                get;
                set;
            }
            public int TravelPictureId //圖片ID
            {
                get;
                set;
            }
            public string TravelProductName
        {
                get;
                set;
            }
            public decimal Price
            {
                get;
                set;
            }
    }
    
}
