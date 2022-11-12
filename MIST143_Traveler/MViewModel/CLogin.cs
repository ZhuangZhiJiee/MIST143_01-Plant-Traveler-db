using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Models.miViewModel
{
    public class CLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
       public string KeepLogin { get; set; }

        public string CAPTCHA { get; set; }
    }
}
