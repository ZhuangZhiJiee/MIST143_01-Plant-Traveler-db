using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class TProduct
    {
        public int FId { get; set; }
        public string FName { get; set; }
        public decimal? FCost { get; set; }
        public int? FQty { get; set; }
        public decimal? FPrice { get; set; }
        public byte[] FImage { get; set; }
        public string FImagePath { get; set; }
    }
}
